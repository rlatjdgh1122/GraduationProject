using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolfBaseState : EnemyState<EnemyWolfStateEnum>
{
    public EnemyWolfBaseState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName) 
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
            _stateMachine.ChangeState(EnemyWolfStateEnum.Dead); //Á×À¸¸é Dead State·Î
    }

    public override void Exit()
    {
        base.Exit();
    }
}
