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
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase); //가는 도중에 감지 사거리 내에 타겟 플레이어가 있으면 Chase로

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Reached);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
*/