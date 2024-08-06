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

        //감지사거리 안에 있을때
        if (_enemy.IsTargetInInnerRange)
        {
            //공격사거리 안에 타겟 플레이어가 있을때
            if (_enemy.IsTargetInAttackRange)
            {
                //나와 마주쳤을때
                if (IsFacingTarget())
                    _stateMachine.ChangeState(EnemyStateType.Attack);
            }

            //공격사거리 밖에 플레이어가 있을때
            else
            {
                _stateMachine.ChangeState(EnemyStateType.Chase);
            }
        }
        //감지사거리 밖에 있을때
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

        //내적
        float dotProduct = Vector3.Dot(enemyForward, directionToTarget);

        return dotProduct > AngleThreshold;
    }*/

    private bool IsFacingTarget()
    {
        Vector3 enemyForward = _enemy.transform.forward;
        Vector3 directionToTarget = (_enemy.CurrentTarget.transform.position - _enemy.transform.position).normalized;

        // 두 벡터 사이의 각도를 계산
        float angleToTarget = Vector3.Angle(enemyForward, directionToTarget);

        // 임계값 설정 (예: 30도)

        // 각도를 사용하여 임계값과 비교
        return angleToTarget <= AngleThreshold;
    }

}
