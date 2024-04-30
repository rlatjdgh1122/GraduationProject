public class EnemyAnimalProvokedState : EnemyAnimalBaseState
{
    public EnemyAnimalProvokedState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //필요없을듯
        //_enemy.CurrentTarget = _enemy.FindNearestPenguin<ShieldPenguin>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.OnProvokedEvent?.Invoke();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Provoked); //공격 사거리 밖이면 계속 쉴드한테만 따라가
    }

    public override void Exit()
    {
        base.Exit();
    }
}
