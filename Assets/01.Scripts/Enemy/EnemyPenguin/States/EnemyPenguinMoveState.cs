
public class EnemyPenguinMoveState : EnemyPenguinBaseState
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

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.ReachedNexus)
        {
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
        }      
    }

    public override void Exit()
    {
        base.Exit();
    }
}
