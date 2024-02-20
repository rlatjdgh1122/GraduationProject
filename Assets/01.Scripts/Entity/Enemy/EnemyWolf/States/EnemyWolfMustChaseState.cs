public class EnemyWolfMustChaseState : EnemyWolfBaseState
{
    public EnemyWolfMustChaseState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName) 
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
            _stateMachine.ChangeState(EnemyWolfStateEnum.Chase);

        if (_enemy.NavAgent.remainingDistance < 0.1f && !_enemy.NavAgent.pathPending)
        {
            _stateMachine.ChangeState(EnemyWolfStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
