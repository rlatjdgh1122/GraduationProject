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
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
