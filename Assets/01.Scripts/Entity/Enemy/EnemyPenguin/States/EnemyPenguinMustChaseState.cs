public class EnemyPenguinMustChaseState : EnemyPenguinBaseState
{
    public EnemyPenguinMustChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _enemy.CurrentTarget = _enemy.FindNearestPenguin<Penguin>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.SetTarget(_enemy.CurrentTarget.transform.position);

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.NavAgent.remainingDistance < 0.1f && !_enemy.NavAgent.pathPending)
        {
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
