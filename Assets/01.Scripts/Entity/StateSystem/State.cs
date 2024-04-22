using UnityEngine;
using UnityEngine.AI;

public class State
{
    protected PenguinStateMachine _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

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
        SignalHub.OnIceArrivedEvent += FindTarget;

        if (_navAgent != null)
        {
            _navAgent?.ResetPath();
            //_navAgent.isStopped = false;
            //_penguin.SetNavmeshPriority(Penguin.PriorityType.High);
        }

        _penguin.CurrentTarget = null;
        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;
    }
    protected void AttackEnter()
    {
        //���� ������ �̺�Ʈ�� ����
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.StopImmediately();

        //�̷��� �ϸ� Attack�ִϸ��̼� ���� �� �ִϸ��̼Ǳ��� attackSpeed�� ������
        //�׷��� �ִϸ��̼ǿ��� �ӵ��� �ٿ���
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }
    protected void ChaseEnter()
    {
        //���� �ʿ��Ѱ�?
        _triggerCalled = true;

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _penguin.ArmyTriggerCalled = false;
            _penguin.WaitForCommandToArmyCalled = false;
        }

        //���� �ʿ��Ѱ�?
        //���� ����� Ÿ���� ã��
        _penguin.FindFirstNearestEnemy();

        _penguin.StartImmediately();

        //����
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();
    }
    protected void MoveEnter()
    {
        //if (_penguin.MoveFocusMode != MovefocusMode.Battle) return;

        _triggerCalled = true;
        _penguin.SuccessfulToArmyCalled = false;

        if (_penguin.WaitForCommandToArmyCalled)
            _penguin.MoveToMouseClickPositon();
    }
    protected void MustMoveEnter()
    {
        _penguin.MoveToMouseClickPositon();
    }
    protected void DeadEnter()
    {
        _triggerCalled = true;
        _penguin.CurrentTarget = null;
        _penguin.enabled = false;
        _penguin.NavAgent.enabled = false;
    }
    #endregion

    #region Exit
    protected void IdleExit()
    {
        //_penguin.SetNavmeshPriority(Penguin.PriorityType.Low);
        SignalHub.OnIceArrivedEvent -= FindTarget;
    }
    protected void AttackExit()
    {
        _penguin.AnimatorCompo.speed = 1;
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDiedEndEvent -= DeadTarget;
    }
    #endregion

    /// <summary>
    /// ��Ʋ����� �� ������ ���콺 Ŭ���� �ߴٸ�
    /// </summary>
    protected bool IsArmyCalledIn_BattleMode()
       => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Battle;

    /// <summary>
    /// ��ɸ���� �� ������ ���콺 Ŭ���� �ߴٸ�
    /// </summary>
    protected bool IsArmyCalledIn_CommandMode()
        => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Command;

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

    protected void DeadTarget()
    {
        var prevTarget = _penguin.CurrentTarget;

        _penguin.FindFirstNearestEnemy();

        if (prevTarget != null)
        {
            prevTarget.HealthCompo.OnDied -= DeadTarget;
        }
        if (_penguin.CurrentTarget != null)
        {
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;
        }
    }
    protected void FindTarget()
    {
        _penguin.FindFirstNearestEnemy();
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