
public class EnemyBasicIdleState : EnemyBasicBaseState
{
    public EnemyBasicIdleState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsMove)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //IsMove �� ������ True�̸� �ؼ����� Move

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //���� ��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> �Ѿư�
    }

    public override void Exit()
    {
        base.Exit();
    }
}
