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
        _worker.ChangeNavqualityToNone();
        _triggerCalled = true;
        _worker.MoveToNexus();
        Debug.Log("돌아가기 시작");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_worker.CanWork == false
            && _worker.EndWork == true
            && _worker.CheckNexusDistance() < .5f)
            _worker.MoveEndToNexus();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
