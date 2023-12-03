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
        if (_enemy.ReachedNexus)
            _enemy.LookAtNexus();
        else if (_enemy.IsTargetPlayerInside)
            _enemy.LookTarget();

        if (!_enemy.ReachedNexus)
        {
            if (!_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

            if (!_enemy.IsAttackable)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
