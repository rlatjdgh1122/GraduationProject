using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PenguinState<T, G> : EntityState<T, G> where T : Enum where G : Penguin
{
    public PenguinState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    #region Enter
    protected void IdleEnter()
    {
        SignalHub.OnIceArrivedEvent += FindTarget;

        if (_navAgent != null)
        {
            _navAgent.ResetPath();
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
        //적이 죽을때 이벤트를 연결
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.StopImmediately();

        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }
    protected void ChaseEnter()
    {
        //굳이 필요한가?
        _triggerCalled = true;

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _penguin.ArmyTriggerCalled = false;
            _penguin.WaitForCommandToArmyCalled = false;
        }

        //굳이 필요한가?
        //가장 가까운 타겟을 찾음
        _penguin.SetTarget();

        _penguin.StartImmediately();

        //따라감
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
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;
    }
    #endregion

    /// <summary>
    /// 배틀모드일 때 유저가 마우스 클릭을 했다면
    /// </summary>
    protected bool IsArmyCalledIn_BattleMode()
       => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Battle;

    /// <summary>
    /// 명령모드일 때 유저가 마우스 클릭을 했다면
    /// </summary>
    protected bool IsArmyCalledIn_CommandMode()
        => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Command;

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

        _penguin.SetTarget();

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
        _penguin.SetTarget();
    }
}