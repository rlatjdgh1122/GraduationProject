using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdleState : BotBaseState
{
    public BotIdleState(Enemy enemyBase, EnemyStateMachine<BotPenguinStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _stateMachine.ChangeState(BotPenguinStateEnum.Chase);
    }
    public override void Exit()
    {
        base.Exit();
    }

 
}
