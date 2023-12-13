
public class EnemyPenguinMoveState : EnemyPenguinBaseState
{
    public EnemyPenguinMoveState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately(); //움직이면서 공격하는거 방지
        _enemy.MoveToNexus();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //가는 도중에 감지 사거리 내에 타겟 플레이어가 있으면 Chase로
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
