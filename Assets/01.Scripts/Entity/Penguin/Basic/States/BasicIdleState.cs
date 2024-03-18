public class BasicIdleState : BasicBaseState
{
    public BasicIdleState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (_penguin.IsFreelyMove) { return; }

        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
            _penguin.NavAgent.ResetPath();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);

        //����Ÿ��� ������ /*-������� ��ġ�� �̵��ߴٸ�-*/ �ּ�
        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

        if (_penguin.IsFreelyMove)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.FreelyMove);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
