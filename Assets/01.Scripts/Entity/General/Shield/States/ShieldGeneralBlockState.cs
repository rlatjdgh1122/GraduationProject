public class ShieldGeneralBlockState : ShieldGeneralBaseState
{
    public ShieldGeneralBlockState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.SetTarget();
        _penguin.StopImmediately();

        _penguin.skill.OnSkillCompleted += SpinAttack;
        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.MustMove);
        }
        else
        {
            //죽지 않았다면 아니 근데 이거 업데이트에서 해주고 있을텐데
            if (!_penguin.IsDead)
            {
                //사거리가 멀어지면 맞으러 감
                if (!_penguin.IsInnerMeleeRange)
                    if (!_penguin.IsDead)
                        _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);

                IsTargetNull(ShieldGeneralPenguinStateEnum.Idle);
            }

        }
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.SpinAttack);
    }

    private void ImpactShield()
    {
        if (!_penguin.canSpinAttack)
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillCompleted -= SpinAttack;
        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
