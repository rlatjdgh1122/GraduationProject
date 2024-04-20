using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaChestHitState : EnemyGorillaBaseState
{
    public EnemyGorillaChestHitState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.StopImmediately();
        _triggerCalled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            if (_enemy.CanAttack) //사거리 안에 있다
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Attack);
            else
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _triggerCalled = false;
    }
}
