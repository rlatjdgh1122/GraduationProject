using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtState : EnemyBaseState
{
    private readonly float AngleThreshold = 10f;

    public EnemyLookAtState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _triggerCalled = false;
        _enemy.StopImmediately();
        _enemy.FindNearestTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        //������Ÿ� �ȿ� ������
        if (_enemy.IsTargetInInnerRange)
        {
            //���ݻ�Ÿ� �ȿ� Ÿ�� �÷��̾ ������
            if (_enemy.IsTargetInAttackRange)
            {
                //���� ����������
                if (IsFacingTarget())
                    _stateMachine.ChangeState(EnemyStateType.Attack);
            }

            //���ݻ�Ÿ� �ۿ� �÷��̾ ������
            else
            {
                _stateMachine.ChangeState(EnemyStateType.Chase);
            }
        }
        //������Ÿ� �ۿ� ������
        else
        {
            _stateMachine.ChangeState(EnemyStateType.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    /*private bool IsFacingTarget()
    {
        Vector3 enemyForward = _enemy.transform.forward;

        Vector3 directionToTarget = (_enemy.CurrentTarget.transform.position - _enemy.transform.position).normalized;   

        //����
        float dotProduct = Vector3.Dot(enemyForward, directionToTarget);

        return dotProduct > AngleThreshold;
    }*/

    private bool IsFacingTarget()
    {
        Vector3 enemyForward = _enemy.transform.forward;
        Vector3 directionToTarget = (_enemy.CurrentTarget.transform.position - _enemy.transform.position).normalized;

        // �� ���� ������ ������ ���
        float angleToTarget = Vector3.Angle(enemyForward, directionToTarget);

        // �Ӱ谪 ���� (��: 30��)

        // ������ ����Ͽ� �Ӱ谪�� ��
        return angleToTarget <= AngleThreshold;
    }

}
