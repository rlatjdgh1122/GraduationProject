public class EnemyMustChaseState : EnemyBaseState
{
    public EnemyMustChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _triggerCalled = true;
        _enemy.FindNearestTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Chase);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
