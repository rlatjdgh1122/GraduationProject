public class ShieldImpactState : ShieldBaseState
{
    public ShieldImpactState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _triggerCalled = false;
        _penguin.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
