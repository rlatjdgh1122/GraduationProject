using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaChestHitState : EnemyGorillaBaseState
{
    public EnemyGorillaChestHitState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log(_enemy.AnimatorCompo.GetBool("Attack"));
        _triggerCalled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(EnemyGorillaStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _triggerCalled = false;
    }
}
