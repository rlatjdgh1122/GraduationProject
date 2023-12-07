using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPenguinMustChaseState : EnemyPenguinBaseState
{
    private Penguin _nearestPayer;

    public EnemyPenguinMustChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _nearestPayer = _enemy.FindNearestPenguin("Player");
        if (_nearestPayer != null)
            _enemy.SetTarget(_nearestPayer.transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetPlayerInside && _enemy.Target != null)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.NavAgent.remainingDistance < 0.1f && !_enemy.NavAgent.pathPending)
        {
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
        }


        if (_enemy.IsDead)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
