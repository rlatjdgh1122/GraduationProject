/*
using System;
using UnityEngine;

public class EnemyAnimalIdleState : EnemyAnimalBaseState
{
    public EnemyAnimalIdleState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.IsMove)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //IsMove �� ������ True�̸� �ؼ����� Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
*/