using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlockState : ShieldBaseState
{
    public ShieldBlockState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    int StunAtk = 1;

    public override void Enter()
    {
        base.Enter();

        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.StopImmediately();
        _penguin.FindNearestEnemy();

        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;
        else
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);

        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.MustMove);
        }
        else
        {
            //사거리가 멀어지면 맞으러 감
            if (!_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

            IsTargetNull(ShieldPenguinStateEnum.Idle);
        }


        if (StunAtk > 0 && _penguin.CheckStunPassive(_penguin.HealthCompo.maxHealth, _penguin.HealthCompo.currentHealth))
        {
            _penguin?.OnPassiveStunEvent();
            StunAtk--;
        }
    }

    private void ImpactShield()
    {
        _stateMachine.ChangeState(ShieldPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;

        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
