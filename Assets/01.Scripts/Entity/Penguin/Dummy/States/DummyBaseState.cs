using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyBaseState : PenguinState<DummyPenguinStateEnum, Penguin>
{
    public DummyBaseState(Penguin penguin, EntityStateMachine<DummyPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(DummyPenguinStateEnum.GoToHouse);
    }
    public override void Exit()
    {
        base.Exit();
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
        return hit.position; // NavMesh 위의 랜덤 위치를 반환
    }

}
