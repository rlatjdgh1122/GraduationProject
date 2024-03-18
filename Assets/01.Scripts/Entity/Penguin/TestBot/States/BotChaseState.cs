using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotChaseState : BotBaseState
{
    public BotChaseState(Enemy enemyBase, EnemyStateMachine<BotPenguinStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        _enemy.CurrentTarget = _enemy.FindNearestPenguin<Penguin>();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(BotPenguinStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(BotPenguinStateEnum.Chase); //공격 사거리 밖이면 계속 따라가

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(BotPenguinStateEnum.Provoked); //도발당할 시 도발State로

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(BotPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
