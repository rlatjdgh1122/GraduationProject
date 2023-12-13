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

        if (_triggerCalled) //공격이 한 차례 끝났을 때,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //사거리 안에 타겟 플레이어가 있다 -> 따라가
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //없다 -> 넥서스로 Move
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
