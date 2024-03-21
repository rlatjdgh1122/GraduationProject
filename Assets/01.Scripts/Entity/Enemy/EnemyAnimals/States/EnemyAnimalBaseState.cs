using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimalBaseState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyAnimalBaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
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
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead); //Á×À¸¸é Dead State·Î
    }

    public override void Exit()
    {
        base.Exit();
    }
}
