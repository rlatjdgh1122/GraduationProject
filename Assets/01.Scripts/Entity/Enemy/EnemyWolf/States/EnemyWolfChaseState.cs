
using UnityEngine;

public class EnemyWolfChaseState : EnemyWolfBaseState
{
    public EnemyWolfChaseState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.SetTarget(_enemy.CurrentTarget.transform.position);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        else
            _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //���� ��Ÿ� ���̸� ��� ����

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Provoked); //���ߴ��� �� ����State��

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Move); //�÷��̾� ����� �ƿ� ���� ��Ÿ��� ����� -> �ؼ����� Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
