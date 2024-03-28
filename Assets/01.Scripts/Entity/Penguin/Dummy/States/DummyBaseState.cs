using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyBaseState : DummyState
{
    public DummyBaseState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();

        _triggerCalled = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsGoToHouse)
        {
            _stateMachine.ChangeState(DummyPenguinStateEnum.GoToHouse);
            //return;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    protected void StopImmediately()
    {
        if (_navAgent != null)
        {
            if (_navAgent.isActiveAndEnabled)
            {
                _navAgent.isStopped = true;
            }
        }
    }
    protected void MoveToPosition(Vector3 pos)
    {
        if (_navAgent.isActiveAndEnabled)
        {
            _navAgent?.ResetPath();
            _navAgent?.SetDestination(pos);
        }
    }

    protected DummyPenguinStateEnum RandomState()
    {
        var RandomValue = Random.Range(0, 4);

        switch (RandomValue)
        {
            case 0: return DummyPenguinStateEnum.FreelyIdle;
            case 1: return DummyPenguinStateEnum.Walk;
            case 2: return DummyPenguinStateEnum.Running;
            case 3: return DummyPenguinStateEnum.DumbToDo;
            default: return DummyPenguinStateEnum.FreelyIdle;
        }

    }
    protected Vector3 GetRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * 10.0f + _penguin.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, 10.0f, NavMesh.AllAreas);
        return hit.position; // NavMesh ���� ���� ��ġ�� ��ȯ
    }

}