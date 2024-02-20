using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWolfReachedState : EnemyWolfBaseState
{
    public EnemyWolfReachedState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _enemy.CurrentTarget = _enemy.FindNearestPenguin<Penguin>();
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
                _stateMachine.ChangeState(EnemyWolfStateEnum.Chase);
            else
                _stateMachine.ChangeState(EnemyWolfStateEnum.Move);
        }
    }

    public void ChangeStateWhenHitted()
    {
        _stateMachine.ChangeState(EnemyWolfStateEnum.MustChase);
    }

    public override void Exit()
    {
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
