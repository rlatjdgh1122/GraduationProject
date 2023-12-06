public class EnemyArcherChaseState : EnemyBasicBaseState
{
    public EnemyArcherChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
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
            _enemy.StopImmediately();
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
        }
        else if (!_enemy.IsAttackable)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (!_enemy.IsTargetPlayerInside && _enemy.Target != null)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

        if (_enemy.ReachedNexus)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
