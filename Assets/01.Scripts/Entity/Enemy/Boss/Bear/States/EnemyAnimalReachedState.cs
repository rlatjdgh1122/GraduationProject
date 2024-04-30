using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAnimalReachedState : EnemyAnimalBaseState
{
    public EnemyAnimalReachedState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _enemy.FindNearestTarget();
        _enemy.HealthCompo.OnHit += ChangeStateWhenHitted;
        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookAtNexus();

        if (_triggerCalled)
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
        }
    }

    public void ChangeStateWhenHitted()
    {
        _stateMachine.ChangeState(EnemyPenguinStateEnum.MustChase);
    }

    public override void Exit()
    {
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
