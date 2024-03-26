public class GeneralBlockState : GeneralBaseState
{
    public GeneralBlockState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _triggerCalled = true;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();

        _penguin.skill.OnSkillCompleted += SpinAttack;
        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (!_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.SpinAttack);
    }

    private void ImpactShield()
    {
        if(!_penguin.canSpinAttack)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillCompleted -= SpinAttack;
        //_penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
