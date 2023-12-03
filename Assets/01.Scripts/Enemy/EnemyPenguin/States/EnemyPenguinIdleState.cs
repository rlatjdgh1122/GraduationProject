
public class EnemyPenguinIdleState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyPenguinIdleState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //if (_enemy.isMove)
        //    _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
