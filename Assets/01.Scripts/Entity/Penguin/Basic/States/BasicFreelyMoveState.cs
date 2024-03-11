using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BasicFreelyMoveState : BasicBaseState
{
    public BasicFreelyMoveState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.MoveToTarget(GetRandomPoint());
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * 50.0f + _penguin.transform.position;

        NavMeshHit hit;
        
        NavMesh.SamplePosition(randomPos, out hit, 50.0f, NavMesh.AllAreas);

        return hit.position;
    }
}
