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
        _enemy.HealthCompo.OnHit += ChangeStateWhenHitted;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsDead)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead); //Á×À¸¸é Dead State·Î
    }

    public void ChangeStateWhenHitted()
    {
        if (_triggerCalled)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
    }
}
