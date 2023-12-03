
using UnityEngine;

public class EnemyPenguinChaseState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyPenguinChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Transform nearestPlayer = _enemy.FindNearestObjectByTag("Player");
        _enemy.SetTarget(nearestPlayer.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

        if (_enemy.IsAttackable)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
