
public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();
    }
   
    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.NexusTarget != null)
            _enemy.MoveToNexus();

        if (_enemy.IsTargetInInnerRange)    
            _stateMachine.ChangeState(EnemyStateType.Chase); //���� ���߿� ���� ��Ÿ� ���� Ÿ�� �÷��̾ ������ Chase��

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached);
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}
