
public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();

        SignalHub.OnIceArrivedEvent += ChangeState;
    }

    public override void UpdateState()
    {
        base.UpdateState();

       /* if (_enemy.IsTargetInInnerRange)
        {
            _stateMachine.ChangeState(EnemyStateType.Chase);
        }
        else
            _stateMachine.ChangeState(EnemyStateType.Move);*/

        //if (_enemy.IsMove)
        //    _stateMachine.ChangeState(EnemyStateType.Move); //IsMove 불 변수가 True이면 넥서스로 Move
    }

    private void ChangeState()
    {
        _enemy.NavAgent.enabled = true;
        _enemy.FindNearestTarget();

        _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();

        SignalHub.OnIceArrivedEvent -= ChangeState;
    }
}
