public class ArcherChaseState : ArcherBaseState
{
    public ArcherChaseState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.nearestEnemy = _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.Target == null)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);

        if (_penguin.nearestEnemy != null)
            _penguin.SetTarget(_penguin.nearestEnemy.transform.position);

        if (_penguin.IsAttackRange)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
