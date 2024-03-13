using System;

public class PenguinState<T, G> : EntityState<T, G> where T : Enum where G : Penguin
{
    public PenguinState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }
    protected bool IsArmyCalledIn_BattleMode()
       => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Battle;
    protected bool IsArmyCalledIn_CommandMode()
        => _penguin.ArmyTriggerCalled && _penguin.MoveFocusMode == MovefocusMode.Command;

    protected bool IsTargetNull(T stateEnum)
    {
        if (_penguin.CurrentTarget != null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }
    protected bool IsTargetNotNull(T stateEnum)
    {
        if (_penguin.CurrentTarget == null) return false;
        _stateMachine.ChangeState(stateEnum);
        return true;
    }


    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
       
    }
}