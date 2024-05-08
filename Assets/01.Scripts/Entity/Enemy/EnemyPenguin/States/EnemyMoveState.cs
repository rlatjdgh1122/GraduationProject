
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
            _stateMachine.ChangeState(EnemyStateType.Reached); //공격 사거리 내에 들어왔다 -> Reached (넥서스 공격)

        if (_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Chase); //감시 사거리 내에 들어왔다 -> chase (플레이어 쪽으로)

      /*  if (_enemy.IsTargetInInnerRange)    
            _stateMachine.ChangeState(EnemyStateType.Chase); //가는 도중에 감지 사거리 내에 타겟 플레이어가 있으면 Chase로

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached);*/
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}
