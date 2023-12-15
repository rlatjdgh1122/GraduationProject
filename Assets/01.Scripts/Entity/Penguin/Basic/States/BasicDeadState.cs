public class BasicDeadState : BasicBaseState
{
    public BasicDeadState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.Target = null;
        _penguin.tag = "Untagged";
        _penguin.enabled = false;
        _penguin.CharController.enabled = false;
        _penguin.NavAgent.enabled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
