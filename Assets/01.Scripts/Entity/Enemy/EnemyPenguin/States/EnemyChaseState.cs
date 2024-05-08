
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

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsTargetInAttackRange)
            _stateMachine.ChangeState(EnemyStateType.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else if (!_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
