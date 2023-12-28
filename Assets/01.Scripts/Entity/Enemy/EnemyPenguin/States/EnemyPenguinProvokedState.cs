public class EnemyPenguinProvokedState : EnemyPenguinBaseState
{
    public EnemyPenguinProvokedState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.CurrentTarget = _enemy.FindNearestPenguin<ShieldPenguin>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.OnProvokedEvent?.Invoke();

        if (_enemy.CurrentTarget != null)
            _enemy.SetTarget(_enemy.CurrentTarget.transform.position);
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Provoked); //���� ��Ÿ� ���̸� ��� �������׸� ����
    }

    public override void Exit()
    {
        base.Exit();
    }
}