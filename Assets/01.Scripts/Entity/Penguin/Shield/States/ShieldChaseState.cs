public class ShieldChaseState : ShieldBaseState
{
    public ShieldChaseState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.MoveFocusMode == ArmySystem.MovefocusMode.Command)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsTargetInAttackRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);

            else IsTargetNull(ShieldPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
