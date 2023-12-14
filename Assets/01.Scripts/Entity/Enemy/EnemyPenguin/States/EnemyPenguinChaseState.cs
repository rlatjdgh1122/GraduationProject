
using UnityEngine;

public class EnemyPenguinChaseState : EnemyPenguinBaseState
{
    private Penguin _nearestPlayer;

    public EnemyPenguinChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _nearestPlayer = _enemy.FindNearestPenguin("Player");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_nearestPlayer != null)
            _enemy.SetTarget(_nearestPlayer.transform.position);

        if (_enemy.IsAttackable)
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
