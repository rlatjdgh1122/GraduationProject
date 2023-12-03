using UnityEngine;

public class EnemyPenguinAttackState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyPenguinAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.SetRotation();

        if (!_enemy.IsInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
