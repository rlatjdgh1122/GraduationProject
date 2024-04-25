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
            //���� �ʾҴٸ� �ƴ� �ٵ� �̰� ������Ʈ���� ���ְ� �����ٵ�
            if (!_penguin.IsDead)
            {
                //��Ÿ��� �־����� ������ ��
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
