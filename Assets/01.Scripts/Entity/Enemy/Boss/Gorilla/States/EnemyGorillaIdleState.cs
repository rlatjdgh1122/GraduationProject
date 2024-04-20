using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaIdleState : EnemyGorillaBaseState
{
    public EnemyGorillaIdleState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IdleEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase);

        if (_enemy.IsMove)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Move); //IsMove �� ������ True�̸� �ؼ����� Move
    }
    public override void Exit()
    {
        base.Exit();
    }
}
