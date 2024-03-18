using System;

public class PenguinState<T, G> : EntityState<T, G> where T : Enum where G : Penguin
{
    public PenguinState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    protected void ChaseEnter()
    {
        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _penguin.ArmyTriggerCalled = false;
            _penguin.WaitForCommandToArmyCalled = false;
        }
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
    protected void MoveEnter()
    {
        if (_penguin.MoveFocusMode != MovefocusMode.Battle) return;

        if (_penguin.WaitForCommandToArmyCalled)
            _penguin.MoveToPosition(_penguin.GetSeatPosition());
    }


    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

    }
}