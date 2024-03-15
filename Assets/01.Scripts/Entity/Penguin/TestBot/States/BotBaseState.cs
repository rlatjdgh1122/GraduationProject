using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBaseState : EnemyState<BotPenguinStateEnum>
{
    public BotBaseState(Enemy enemyBase, EnemyStateMachine<BotPenguinStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _stateMachine.ChangeState(BotPenguinStateEnum.Dead); //������ Dead State��
    }
    public override void Exit()
    {
        base.Exit();
    }

  
}
