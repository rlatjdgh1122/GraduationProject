using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaAttackState :EnemyGorillaBaseState
{
    public EnemyGorillaAttackState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackComboEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        if (_enemy.CurrentTarget.IsDead)
        {
            _enemy.CurrentTarget = null;

            _stateMachine.ChangeState(EnemyGorillaStateEnum.Move);
        }

        if (_triggerCalled) //������ �� ���� ������ ��,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����
            else
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Move); //���� -> �ؼ����� Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        AttackComboExit();

        base.Exit();
    }
}
