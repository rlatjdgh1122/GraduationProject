public class GeneralBlockState : GeneralBaseState
{
    public GeneralBlockState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.FindFirstNearestEnemy();
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
            _stateMachine.ChangeState(GeneralPenguinStateEnum.MustMove);
        }
        else
        {
            //죽지 않았다면 아니 근데 이거 업데이트에서 해주고 있을텐데
            if (!_penguin.IsDead)
            {
                //사거리가 멀어지면 맞으러 감
                if (!_penguin.IsInnerMeleeRange)
                    if (!_penguin.IsDead)
                        _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

                IsTargetNull(GeneralPenguinStateEnum.Idle);
            }

        }
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.SpinAttack);
    }

    private void ImpactShield()
    {
        if (!_penguin.canSpinAttack)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillCompleted -= SpinAttack;
        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
