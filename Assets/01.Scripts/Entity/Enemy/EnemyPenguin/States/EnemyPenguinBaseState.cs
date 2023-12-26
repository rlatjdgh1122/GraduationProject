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
        _enemy.OnProvoked += Provoked;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsDead)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead); //Á×À¸¸é Dead State·Î
    }

    private void Provoked()
    {
        _enemy.CurrentTarget = _enemy.FindNearestPenguin<ShieldPenguin>(); //OnProvoked bool·Î »©±â
        _enemy.OnProvokedEvent?.Invoke();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.OnProvoked -= Provoked;
    }
}
