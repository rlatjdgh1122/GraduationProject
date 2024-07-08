using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.MoveToNexus();

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached);

        if (_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Chase);
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}
