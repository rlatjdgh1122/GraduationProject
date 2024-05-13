using ArmySystem;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlockState : ShieldBaseState
{
    int StunCount = 1;
    public ShieldBlockState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

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

        if (_penguin.MoveFocusMode == MovefocusMode.Command)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        }
        else
        {
            //사거리가 멀어지면 맞으러 감
            if (!_penguin.IsTargetInAttackRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

            IsTargetNull(ShieldPenguinStateEnum.Idle);
        }
    }

    private void ImpactShield()
    {
        if (StunCount > 0 && _penguin.CheckHealthRatioPassive(_penguin.HealthCompo.maxHealth, _penguin.HealthCompo.currentHealth))
        {
            _penguin?.OnPassiveHealthRatioEvent();
            StunCount--;
        }
        else
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Impact);
        }

    }

    public override void Exit()
    {
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;

        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
