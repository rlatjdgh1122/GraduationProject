
using System;
using UnityEngine;

public class PenguinState<T, G> : EntityState<T, G> where T : Enum where G : Penguin
{
    public PenguinState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }
    protected void IdleEnter()
    {
        _penguin.NavAgent.ResetPath();

        _navAgent.isStopped = false;
        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;

    }
    protected void AttackEnter()
    {
        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }
    protected void ChaseEnter()
    {
        _triggerCalled = true;

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _penguin.ArmyTriggerCalled = false;
            _penguin.WaitForCommandToArmyCalled = false;
        }

        _penguin.FindFirstNearestEnemy();
    }
    protected void MoveEnter()
    {
        if (_penguin.MoveFocusMode != MovefocusMode.Battle) return;

        _triggerCalled = true;
        _penguin.SuccessfulToArmyCalled = false;

        if (_penguin.WaitForCommandToArmyCalled)
            _penguin.MoveToMouseClickPositon();
    }
    protected void MustMoveEnter()
    {
        //_navAgent.ResetPath();
        _navAgent.isStopped = false;
        //_navAgent.velocity = Vector3.one * .2f;
        _penguin.MoveToMouseClickPositon();
    }
    protected void DeadEnter()
    {
        _triggerCalled = true;
        _penguin.CurrentTarget = null;
        _penguin.enabled = false;
        _penguin.NavAgent.enabled = false;
        //_penguin.CharController.enabled = false;
    }

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

    /// <summary>
    /// 타겟이 있다면
    /// </summary>
    /// <param name="stateEnum"> 이 상태로 체인지</param>
    /// <returns></returns>
    protected bool IsTargetNotNull(T stateEnum)
    {
        if (_penguin.CurrentTarget == null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }
    /// <summary>
    /// 배틀모드일 때 다 죽이면 위치로 이동하는 함수
    /// </summary>



    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

    }
}