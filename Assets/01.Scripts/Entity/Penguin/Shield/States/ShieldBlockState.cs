public class ShieldBlockState : ShieldBaseState
{
    public ShieldBlockState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindFirstNearestEnemy();
        _penguin.owner.IsMoving = false;
        _penguin.StopImmediately();

        foreach (var enemy in _penguin.FindNearestEnemy(5)) //일단 임시로 5마리도발 이것도 SO로 뺄거임
        {
            enemy.IsProvoked = true;
        }

        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (!_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
    }

    private void ImpactShield()
    {
        _stateMachine.ChangeState(ShieldPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
