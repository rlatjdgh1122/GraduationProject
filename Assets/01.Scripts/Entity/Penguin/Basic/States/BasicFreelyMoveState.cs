using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicFreelyMoveState : BasicBaseState
{
    private float time = 0.0f;
    private float randomMoveTime;

    public BasicFreelyMoveState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        if (_penguin.NavAgent.speed > 1.0f)
        {
            _penguin.NavAgent.speed = 1.0f;
        }
        randomMoveTime = Random.Range(1.0f, 3.0f);
        _penguin.MoveToTarget(GetRandomPoint());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        time += Time.deltaTime;
        if(time >= randomMoveTime ||
           _penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        time = 0.0f;
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * 10.0f + _penguin.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, 10.0f, NavMesh.AllAreas);
        return hit.position; // NavMesh 위의 랜덤 위치를 반환합니다.
    }
}
