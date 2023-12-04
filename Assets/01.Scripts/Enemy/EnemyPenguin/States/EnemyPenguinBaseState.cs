using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguinBaseState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyPenguinBaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsDead)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
