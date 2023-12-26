using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockState : ShieldBaseState
{
    private List<Enemy> enemies;

    public ShieldBlockState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.FindNearestEnemy();
        _penguin.owner.IsMoving = false;
        _penguin.StopImmediately();

        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        enemies = _penguin.FindNearestEnemy(5);

        _penguin.LookTarget();

        if (!_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        else
        {
            foreach (var enemy in enemies)
            {
                enemy.OnProvoked?.Invoke();
            }
        }
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
