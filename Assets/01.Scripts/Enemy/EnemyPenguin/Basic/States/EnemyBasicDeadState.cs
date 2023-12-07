using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicDeadState : EnemyBasicBaseState
{
    public EnemyBasicDeadState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        if (_enemy.Target != null)
            _enemy.Target.FindNearestEnemy("Enemy");
        _enemy.tag = "Untagged";
        _enemy.enabled = false;
        _enemy.CharController.enabled = false;
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
