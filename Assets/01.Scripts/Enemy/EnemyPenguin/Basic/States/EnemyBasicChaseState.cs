
using UnityEngine;

public class EnemyBasicChaseState : EnemyBasicBaseState
{
    public EnemyBasicChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        Penguin nearestPlayer = _enemy.FindNearestPenguin("Player");
        if (nearestPlayer != null)
            _enemy.SetTarget(nearestPlayer.transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsAttackable)
        {
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        }
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
