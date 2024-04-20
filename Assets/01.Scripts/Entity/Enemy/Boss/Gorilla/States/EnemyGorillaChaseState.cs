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
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Provoked); //���ߴ��� �� ����State��


        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Move); //�÷��̾� ����� �ƿ� ���� ��Ÿ��� ����� -> �ؼ����� Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
