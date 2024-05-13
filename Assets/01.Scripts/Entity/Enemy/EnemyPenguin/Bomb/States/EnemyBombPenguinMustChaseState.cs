using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombPenguinMustChaseState : EnemyBaseState
{
    public EnemyBombPenguinMustChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        MustChaseEnter();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_enemy.CurrentTarget != null)
        {
            _enemy.MoveToCurrentTarget();
        }

        if (_enemy.IsTargetInInnerRangeWhenTargetNexus) //공격 사거리 내에 들어왔다 -> Attack
        {
            if (_enemy.IsTargetInAttackRange)
                _stateMachine.ChangeState(EnemyStateType.Attack);
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
