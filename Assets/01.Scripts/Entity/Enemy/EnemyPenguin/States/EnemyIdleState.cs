using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
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
        if (_enemy.IsTargetInInnerRange)
        {
            _stateMachine.ChangeState(EnemyStateType.Chase);
        }
        else if (_enemy.IsTargetInInnerRangeWhenTargetNexus)
        {
            _stateMachine.ChangeState(EnemyStateType.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();
    }
}
