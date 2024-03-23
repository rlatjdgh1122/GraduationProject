using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyBaseState : EntityState<DummyPenguinStateEnum, DummyPenguin>
{
    public DummyBaseState(DummyPenguin penguin, EntityStateMachine<DummyPenguinStateEnum, DummyPenguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            case 0: return DummyPenguinStateEnum.Idle;
            case 1: return DummyPenguinStateEnum.Walk;
            case 2: return DummyPenguinStateEnum.Running;
            case 3: return DummyPenguinStateEnum.DumbToDo;
            default: return DummyPenguinStateEnum.Idle;
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
