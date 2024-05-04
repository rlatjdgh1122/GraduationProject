
public class EnemyIdleState : EnemyBaseState
{
    private bool isChase => _enemy.IsMove && _enemy.IsTargetInInnerRange;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /* if (_enemy.IsMove)  
             _stateMachine.ChangeState(EnemyStateType.Move); //IsMove 불 변수가 True이면 넥서스로 Move
 */
        if (isChase)
            _stateMachine.ChangeState(EnemyStateType.Chase); //감지 사거리 안에 타겟 플레이어가 있다 -> 쫓아가  
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();
    }
}
