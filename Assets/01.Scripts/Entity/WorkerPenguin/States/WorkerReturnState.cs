using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("돌아가기 시작");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_worker.CheckNexusDistance() < 0.05f)
            _worker.MoveEndToNexus();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
