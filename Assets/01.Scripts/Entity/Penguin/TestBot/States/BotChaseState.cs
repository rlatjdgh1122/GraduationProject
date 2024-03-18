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
            _stateMachine.ChangeState(BotPenguinStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        else
            _stateMachine.ChangeState(BotPenguinStateEnum.Chase); //���� ��Ÿ� ���̸� ��� ����

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(BotPenguinStateEnum.Provoked); //���ߴ��� �� ����State��

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(BotPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
