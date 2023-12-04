
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

        Penguin nearestPlayer = _enemy.FindNearestPenguin("Player");
        if (nearestPlayer != null)
            _enemy.SetTarget(nearestPlayer.transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsAttackable)
        {
            _enemy.StopImmediately();
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
        }
        else if (!_enemy.IsAttackable)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
