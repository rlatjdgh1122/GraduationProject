
public class EnemyPenguinMoveState : EnemyPenguinBaseState
{
    public EnemyPenguinMoveState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately(); //�����̸鼭 �����ϴ°� ����
        _enemy.MoveToNexus();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //���� ���߿� ���� ��Ÿ� ���� Ÿ�� �÷��̾ ������ Chase��
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

        if (_enemy.ReachedNexus)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Reached);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
