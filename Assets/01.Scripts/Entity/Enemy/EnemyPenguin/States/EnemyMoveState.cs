
public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();

        _enemy.MoveToNexus();
    }
   
    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached); //���� ��Ÿ� ���� ���Դ� -> Reached (�ؼ��� ����)

        if (_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Chase); //���� ��Ÿ� ���� ���Դ� -> chase (�÷��̾� ������)

      /*  if (_enemy.IsTargetInInnerRange)    
            _stateMachine.ChangeState(EnemyStateType.Chase); //���� ���߿� ���� ��Ÿ� ���� Ÿ�� �÷��̾ ������ Chase��

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached);*/
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}
