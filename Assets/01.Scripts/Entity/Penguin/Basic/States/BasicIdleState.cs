public class BasicIdleState : BasicBaseState
{
    public BasicIdleState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);

        if (_penguin.IsInTargetRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
