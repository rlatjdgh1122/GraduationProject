
public class EnemyWolfMoveState : EnemyWolfBaseState
{
    public EnemyWolfMoveState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately(); //�����̸鼭 �����ϴ°� ����
        _triggerCalled = true;

        _enemy.CurrentTarget = _enemy.FindNearestPenguin<Penguin>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.NexusTarget != null)
            _enemy.MoveToNexus();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //���� ���߿� ���� ��Ÿ� ���� Ÿ�� �÷��̾ ������ Chase��

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Reached);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
