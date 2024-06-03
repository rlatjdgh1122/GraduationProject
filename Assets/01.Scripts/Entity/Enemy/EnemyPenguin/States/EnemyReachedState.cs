using System;
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
        _enemy.StopImmediately();
        _enemy.HealthCompo.OnHit += OnHitHandler;

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

    private void OnHitHandler()
    {
        _stateMachine.ChangeState(EnemyStateType.MustChase);
    }


    public override void ExitState()
    {
        base.ExitState();

        _enemy.HealthCompo.OnHit -= OnHitHandler;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        //���ݳ����� ��Ž��
        _enemy.FindNearestTarget();
    }
}
