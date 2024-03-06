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
        //.ArmyTriggerCalled = false;
        _penguin.SuccessfulToSeatMyPostion = false;
        //_penguin.MoveToTarget(_penguin.GetSeatPosition()); //위치로 이동
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
        /* if (_penguin.IsInnerTargetRange
             && !_penguin.ArmyTriggerCalled)
         {
             _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
         }*/
    }

    public override void Exit()
    {
        base.Exit();
    }
}
