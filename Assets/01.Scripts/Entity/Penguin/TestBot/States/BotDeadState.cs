using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDeadState : BotBaseState
{
    public BotDeadState(Enemy enemyBase, EnemyStateMachine<BotPenguinStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _enemy.enabled = false;
        _enemy.NavAgent.enabled = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
