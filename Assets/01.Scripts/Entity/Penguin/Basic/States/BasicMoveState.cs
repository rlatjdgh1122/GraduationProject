public class BasicMoveState : BasicBaseState
{
    public BasicMoveState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
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

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
