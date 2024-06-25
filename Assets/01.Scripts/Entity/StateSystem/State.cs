using UnityEngine;
using UnityEngine.AI;
using ArmySystem;

public class State
{
    protected PenguinStateMachine _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;
    protected MovefocusMode prevMode = MovefocusMode.Command;

    public State(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName)
    {
        _penguin = penguin;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
        _navAgent = _penguin.NavAgent;
    }

    #region Enter

    protected void IdleEnter()
    {
        _penguin.StopImmediately();

        _penguin.PenguinTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;

        _penguin.MyArmy.MovefocusMode = MovefocusMode.Command;
        //_penguin.FindNearestEnemyInTargetArmy();
    }

    protected void AttackEnter()
    {
        //���� ������ �̺�Ʈ�� ����
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.ArmyTriggerCalled = false;
        _penguin.PenguinTriggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemyInTargetArmy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemyInTargetArmy();
        }

        _penguin.StopImmediately();

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemyInTargetArmy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemyInTargetArmy();
        }

        //�̷��� �ϸ� Attack�ִϸ��̼� ���� �� �ִϸ��̼Ǳ��� attackSpeed�� ������
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    protected void ChaseEnter()
    {
        //���� �ʿ��Ѱ�?
        _triggerCalled = true;
        _penguin.ArmyTriggerCalled = false;

        //���� �ʿ��Ѱ�?
        //���� ����� Ÿ���� ã��

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemyInTargetArmy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemyInTargetArmy();
        }


        //����
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();
    }

    protected void MoveEnter()
    {
        //if (_penguin.MoveFocusMode != MovefocusMode.Battle) return;
        _triggerCalled = true;
        _penguin.ArmyTriggerCalled = false;

        //if (_penguin.WaitForCommandToArmyCalled)
        //_penguin.MoveToMouseClickPositon();
    }

    protected void MustMoveEnter()
    {
        _penguin.MoveToMouseClickPositon();
        _penguin.SuccessfulToArmyCalled = false;
    }
    #endregion

    #region Exit

    protected void IdleExit()
    {

    }

    protected void AttackExit()
    {

        _penguin.PenguinTriggerCalled = false;
        _penguin.AnimatorCompo.speed = 1;

        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;
    }

    protected void ChaseExit()
    {

    }

    protected void MoveExit()
    {

    }

    protected void MustMoveExit()
    {

    }

    #endregion

    protected bool IsPrevMoveMode(MovefocusMode mode)
    {
        return prevMode.Equals(mode);
    }

    /// <summary>
    /// Ÿ���� ���ٸ�
    /// </summary>
    /// <param name="stateEnum"> �� ���·� ü����</param>
    protected bool IsTargetNull(PenguinStateType stateEnum)
    {
        if (_penguin.CurrentTarget != null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }

    protected void CheckCommandModeForMovement()
    {
        if (!_penguin.ArmyTriggerCalled) return;

        if (_penguin.MovefocusMode == MovefocusMode.Command)
        {
            if (_penguin.NavAgent.velocity.magnitude > 0.05f)
                _stateMachine.ChangeState(PenguinStateType.Move);
        }//end command
    }

    protected void CheckBattleModeForChase()
    {
        if (!_penguin.ArmyTriggerCalled) return;

        if (_penguin.MovefocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);
        }//end Battle
    }


    protected void DeadTarget()
    {
        var prevTarget = _penguin.CurrentTarget;

        _penguin.FindNearestEnemyInTargetArmy();

        if (prevTarget != null)
        {
            prevTarget.HealthCompo.OnDied -= DeadTarget;
        }
        if (_penguin.CurrentTarget != null)
        {
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;
        }
    }

    public virtual void EnterState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _navAgent = _penguin.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
}