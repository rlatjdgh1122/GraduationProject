using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerReturnState : WorkerState<WorkerPenguinStateEnum>
{
    public WorkerReturnState(Worker worker, WorkerStateMachine<WorkerPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(worker, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _worker.MoveToNexus();

        _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_worker.CanWork == false
            && _worker.EndWork == true
            && _worker.CheckNexusDistance() < 1.2f)
            _worker.MoveEndToNexus();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
