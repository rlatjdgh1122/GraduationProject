using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotProvokedState : BotBaseState
{
    public BotProvokedState(Enemy enemyBase, EnemyStateMachine<BotPenguinStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.CurrentTarget = _enemy.FindNearestPenguin<ShieldPenguin>();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.OnProvokedEvent?.Invoke();

        if (_enemy.CurrentTarget != null)
            _enemy.SetTarget();
        else
            _stateMachine.ChangeState(BotPenguinStateEnum.Chase);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(BotPenguinStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(BotPenguinStateEnum.Provoked); //공격 사거리 밖이면 계속 쉴드한테만 따라가
    }

    public override void Exit()
    {
        base.Exit();
    }


}
