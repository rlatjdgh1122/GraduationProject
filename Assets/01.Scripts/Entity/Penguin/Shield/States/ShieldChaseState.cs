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

        //무조건 현재 적한테 이동
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();


        if (_penguin.IsTargetInAttackRange)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);
        }

        else IsTargetNull(ShieldPenguinStateEnum.Idle);

        CheckCommandModeForMovement();
        CheckCommandModeForChase();
    }


public override void Exit()
{
    base.Exit();
}
}
