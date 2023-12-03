
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

        if (!_enemy.IsInside && _enemy.isMove)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

        if (_enemy.AttackInable)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
