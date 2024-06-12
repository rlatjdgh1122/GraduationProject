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
        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;
    }

    protected void AttackEnter()
    {
        //적이 죽을때 이벤트를 연결
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.PenguinTriggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemy();
        }

        _penguin.StopImmediately();

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemy();
        }

        //이렇게 하면 Attack애니메이션 말고도 딴 애니메이션까지 attackSpeed로 설정됨
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    protected void ChaseEnter()
    {
        //굳이 필요한가?
        _triggerCalled = true;

        //굳이 필요한가?
        //가장 가까운 타겟을 찾음

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemy();
        }


        //따라감
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();
    }

    protected void MoveEnter()
    {
        //if (_penguin.MoveFocusMode != MovefocusMode.Battle) return;
        _triggerCalled = true;

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

    /// <summary>
    /// 타겟이 없다면
    /// </summary>
    /// <param name="stateEnum"> 이 상태로 체인지</param>
    protected bool IsTargetNull(PenguinStateType stateEnum)
    {
        if (_penguin.CurrentTarget != null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }

    protected void CheckCommandModeForMovement()
    {
        if (_penguin.MovefocusMode == MovefocusMode.Command)
        {
            if (_penguin.NavAgent.velocity.magnitude > 0.05f)
                _stateMachine.ChangeState(PenguinStateType.Move);
        }//end command
    }

    protected void CheckCommandModeForChase()
    {
        if (_penguin.MovefocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);
        }//end Battle
    }

    protected void DeadTarget()
    {
        var prevTarget = _penguin.CurrentTarget;

        _penguin.FindNearestEnemy();

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
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _penguin.NavAgent;
    }

    public virtual void UpdateState()
    {
        if (!_penguin.PenguinTriggerCalled && _penguin.ArmyTriggerCalled)
        {
            _penguin.StateMachine.ChangeState(PenguinStateType.MustMove);
        }
    }

    public virtual void ExitState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
}