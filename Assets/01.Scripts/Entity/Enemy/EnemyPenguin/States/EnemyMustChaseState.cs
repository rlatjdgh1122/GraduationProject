public class EnemyMustChaseState : EnemyBaseState
{
    public EnemyMustChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _triggerCalled = true;
        _enemy.FindHitTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.IsTargetInAttackRange)
            _stateMachine.ChangeState(EnemyStateType.Attack);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
