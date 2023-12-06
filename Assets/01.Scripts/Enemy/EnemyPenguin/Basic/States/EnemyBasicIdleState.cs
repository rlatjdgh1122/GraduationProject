
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
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //IsMove 불 변수가 True이면 넥서스로 Move

        if (_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //감지 사거리 안에 타겟 플레이어가 있다 -> 쫓아가
    }

    public override void Exit()
    {
        base.Exit();
    }
}
