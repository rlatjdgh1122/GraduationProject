
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

        if (_enemy.IsTargetInAttackRange)
            _stateMachine.ChangeState(EnemyStateType.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack
        else
            _stateMachine.ChangeState(EnemyStateType.Chase);

        if (!_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
