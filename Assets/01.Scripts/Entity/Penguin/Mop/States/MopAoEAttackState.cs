public class MopAoEAttackState : MopBaseState
{
    public MopAoEAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindFirstNearestEnemy();
        _penguin.Owner.IsMoving = false;
        _penguin.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (!_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
