/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaMoveState : EnemyGorillaBaseState
{
    public EnemyGorillaMoveState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.NexusTarget != null)
            _enemy.MoveToNexus();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase); //���� ���߿� ���� ��Ÿ� ���� Ÿ�� �÷��̾ ������ Chase��

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Reached);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
*/