using UnityEngine;
using UnityEngine.AI;
using ArmySystem;
using System;

public class State
{
    protected PenguinStateMachine _stateMachine;
    protected EntityActionData _entityActionData;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;
    protected MovefocusMode prevMode = MovefocusMode.Command;

    public State(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName)
    {
        _animBoolHash = Animator.StringToHash(animationBoolName);

        _penguin = penguin;
        _stateMachine = stateMachine;

        _entityActionData = penguin.GetComponent<EntityActionData>();
        _navAgent = _penguin.GetComponent<NavMeshAgent>();

    }

    #region Enter

    protected void IdleEnter()
    {
        //_penguin.StopImmediately();

        _penguin.PenguinTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;

        _penguin.MyArmy.MovefocusMode = MovefocusMode.Command;
        //_penguin.FindNearestEnemyInTargetArmy();
    }

    protected void AttackEnter()
    {
        //적이 죽을때 이벤트를 연결
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.ArmyTriggerCalled = false;
        _penguin.PenguinTriggerCalled = false;

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

        //이렇게 하면 Attack애니메이션 말고도 딴 애니메이션까지 attackSpeed로 설정됨
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    protected void ChaseEnter()
    {
        //굳이 필요한가?
        _triggerCalled = true;
        _penguin.ArmyTriggerCalled = false;

        //굳이 필요한가?
        //가장 가까운 타겟을 찾음

        if (!_penguin.TargetLock)
        {
            _penguin.FindNearestEnemyInTargetArmy();
        }
        else
        {
            if (_penguin.CurrentTarget == null)
                _penguin.FindNearestEnemyInTargetArmy();
        }


        //따라감
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
        _penguin.MoveToClickPositon();
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
    /// 타겟이 없다면
    /// </summary>
    /// <param name="stateEnum"> 이 상태로 체인지</param>
    protected bool IsTargetNull(PenguinStateType stateEnum)
    {
        if (_penguin.CurrentTarget != null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }

    protected bool CheckCommandModeForMovement()
    {
        if (!_penguin.ArmyTriggerCalled) return false;

        if (_penguin.MovefocusMode == MovefocusMode.Command)
        {
            if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            {
                _stateMachine.ChangeState(PenguinStateType.Move);

                return true;
            }
        }//end command

        return false;
    }

    protected bool CheckBattleModeForChase()
    {
        if (!_penguin.ArmyTriggerCalled) return false;

        if (_penguin.MovefocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            return true;
        }//end Battle

        return false;
    }


    protected void DeadTarget()
    {
        var prevTarget = _penguin.CurrentTarget;

        try
        {
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
        catch (NullReferenceException ex)
        {
            //Debug.LogError("NullReferenceException caught in DeadTarget: " + ex.Message);
        }
    }

    public virtual void EnterState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것

    }

    public virtual void UpdateState()
    {

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