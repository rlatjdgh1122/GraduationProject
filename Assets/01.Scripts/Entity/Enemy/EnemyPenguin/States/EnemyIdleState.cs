
public class EnemyIdleState : EnemyBaseState
{
    private bool isChase => _enemy.IsMove && _enemy.IsTargetInInnerRange;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /* if (_enemy.IsMove)  
             _stateMachine.ChangeState(EnemyStateType.Move); //IsMove �� ������ True�̸� �ؼ����� Move
 */
        if (isChase)
            _stateMachine.ChangeState(EnemyStateType.Chase); //���� ��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> �Ѿư�  
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();
    }
}
