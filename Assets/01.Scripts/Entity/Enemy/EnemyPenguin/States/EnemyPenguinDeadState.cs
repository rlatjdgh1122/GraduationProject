using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguinDeadState : EnemyPenguinBaseState
{
    public EnemyPenguinDeadState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _enemy.enabled = false;
        //_enemy.CharController.enabled = false;
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
