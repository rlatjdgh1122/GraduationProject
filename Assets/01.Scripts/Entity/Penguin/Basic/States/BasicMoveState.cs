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
        _penguin.SuccessfulToSeatMyPostion = false;
        if (!_penguin.WaitTrueAnimEndTrigger)
            _penguin.MoveToTarget(_penguin.GetSeatPosition());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }

        // A 클릭 : 위치로 가던중 범위에 적이 있다면 죽이고 위치로
        if (_penguin.IsInnerTargetRange
             && _penguin.BattleMode == true)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
