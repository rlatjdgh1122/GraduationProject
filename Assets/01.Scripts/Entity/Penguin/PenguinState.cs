using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using ArmySystem;
public class PenguinState<T, G> : EntityState<T, G> where T : Enum where G : Penguin
{
    public PenguinState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    #region Enter
    protected void IdleEnter()
    {
        //SignalHub.OnGroundArrivedEvent += FindTarget;

        if (_navAgent.isOnNavMesh)
        {
            _navAgent?.ResetPath();
            //_navAgent.isStopped = false;
            //_penguin.SetNavmeshPriority(Penguin.PriorityType.High);
        }

        _penguin.StopImmediately();
        _penguin.CurrentTarget = null;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;
    }

    protected void AttackEnter()
    {
        //적이 죽을때 이벤트를 연결
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.ArmyTriggerCalled = false;
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

        //이렇게 하면 Attack애니메이션 말고도 딴 애니메이션까지 attackSpeed로 설정됨
        //그래서 애니메이션에서 속도를 줄엿음
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
        _triggerCalled = true;

        if (_penguin.WaitForCommandToArmyCalled)
            _penguin.MoveToMouseClickPositon();
    }

    protected void MustMoveEnter()
    {
        _penguin.MoveToMouseClickPositon();
        _penguin.SuccessfulToArmyCalled = false;
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
        //SignalHub.OnGroundArrivedEvent -= FindTarget;
    }
    protected void AttackExit()
    {
        _penguin.AnimatorCompo.speed = 1;
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;
    }
    #endregion

    /// <summary>
    /// 타겟이 없다면
    /// </summary>
    /// <param name="stateEnum"> 이 상태로 체인지</param>
    protected bool IsTargetNull(T stateEnum)
    {
        if (_penguin.CurrentTarget != null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
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
    protected void FindTarget()
    {
        _penguin.FindNearestEnemyInTargetArmy();
    }
}