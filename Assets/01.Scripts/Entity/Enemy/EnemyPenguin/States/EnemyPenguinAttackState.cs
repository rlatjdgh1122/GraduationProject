public class EnemyPenguinAttackState : EnemyPenguinBaseState
{
    public EnemyPenguinAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        if (_triggerCalled) //������ �� ���� ������ ��,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //���� -> �ؼ����� Move
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
