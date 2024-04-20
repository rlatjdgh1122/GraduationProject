using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaProvokedState : EnemyGorillaBaseState
{
    public EnemyGorillaProvokedState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

       ProvokedEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.OnProvokedEvent?.Invoke();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();
        else
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Provoked); //공격 사거리 밖이면 계속 쉴드한테만 따라가
    }

    public override void Exit()
    {
        
        base.Exit();
    }
}
