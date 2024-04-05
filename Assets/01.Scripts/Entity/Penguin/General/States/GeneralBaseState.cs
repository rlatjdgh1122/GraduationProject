public class GeneralBaseState : GeneralState<GeneralPenguinStateEnum, General>
{
    public GeneralBaseState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
