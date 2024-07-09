
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

        if (_enemy.IsTargetInInnerRange) //���� ��Ÿ� ���� ���Դ� -> Attack
        {
            if (_enemy.IsTargetInAttackRange)
                _stateMachine.ChangeState(EnemyStateType.LookAt);
        }
        else // ���� ��Ÿ� ������ ����� -> Move (�ؼ���������)
            _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
