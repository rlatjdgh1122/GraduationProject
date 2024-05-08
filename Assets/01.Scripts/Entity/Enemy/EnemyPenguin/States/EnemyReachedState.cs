using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReachedState : EnemyBaseState
{
    public EnemyReachedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _triggerCalled = false;
        _enemy.FindNearestTarget();
        _enemy.HealthCompo.OnHit += ChangeStateWhenHitted;
        _enemy.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookAtNexus();

        if (_triggerCalled)
        {
            if (_enemy.IsTargetInInnerRange)
                _stateMachine.ChangeState(EnemyStateType.Chase);
        }
    }

    public void ChangeStateWhenHitted()
    {
        if (_triggerCalled)
            _stateMachine.ChangeState(EnemyStateType.Chase);
    }

    public override void ExitState()
    {
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
        _enemy.AnimatorCompo.speed = 1;
        base.ExitState();
    }
}
