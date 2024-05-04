/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaReachedState : EnemyGorillaBaseState
{
    public EnemyGorillaReachedState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ReachedEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookAtNexus();

        if (_triggerCalled)
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase);
            else
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Move);
        }
    }
    public override void Exit()
    {
        ReachedExit();

        base.Exit();
    }
}
*/