public class MopIdleState : MopBaseState
{
    public MopIdleState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //_triggerCalled = true;

        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;

        //if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        _penguin.NavAgent.ResetPath();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(MopPenguinStateEnum.Move);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
