
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
        {
            _enemy.MoveToCurrentTarget();
        }

        if (_enemy.IsTargetInInnerRange) //공격 사거리 내에 들어왔다 -> Attack
        {
            if (_enemy.IsTargetInAttackRange)
                _stateMachine.ChangeState(EnemyStateType.LookAt);
        }
        else // 감지 사거리 내에서 벗어났다 -> Move (넥서스쪽으로)
            _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
