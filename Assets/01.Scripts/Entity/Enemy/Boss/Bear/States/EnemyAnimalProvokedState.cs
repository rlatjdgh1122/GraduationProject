/*public class EnemyAnimalProvokedState : EnemyAnimalBaseState
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

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
    }

    public override void Exit()
    {
        base.Exit();
    }
}
*/