
public class EnemyPenguinMoveState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyPenguinMoveState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MoveToNexus();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.ReachedNexus)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
