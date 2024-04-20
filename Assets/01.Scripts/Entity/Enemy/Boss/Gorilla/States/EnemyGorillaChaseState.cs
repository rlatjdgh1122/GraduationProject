using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaChaseState : EnemyGorillaBaseState
{
    public EnemyGorillaChaseState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Provoked); //도발당할 시 도발State로


        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Move); //플레이어 펭귄이 아예 감지 사거리를 벗어났다 -> 넥서스로 Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
