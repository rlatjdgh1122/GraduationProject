public class ShieldGeneralBaseState : GeneralState<ShieldGeneralPenguinStateEnum, General>
{
    public ShieldGeneralBaseState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    protected void HoldShield()
    {
        if (_penguin.skill.IsAvaliable)
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Block);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
