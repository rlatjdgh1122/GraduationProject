
using UnityEngine;

public class EnemyPenguinChaseState : EnemyPenguinBaseState
{
    public EnemyPenguinChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        _enemy.FindNearestPenguin();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //���� ��Ÿ� ���̸� ��� ����

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //�÷��̾� ����� �ƿ� ���� ��Ÿ��� ����� -> �ؼ����� Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
