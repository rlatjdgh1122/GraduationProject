using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerIdleState : WorkerState<WorkerPenguinStateEnum>
{
    public WorkerIdleState(Worker worker, WorkerStateMachine<WorkerPenguinStateEnum> stateMachine, string animationBoolName) : base(worker, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_worker.CanWork)
            _stateMachine.ChangeState(WorkerPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
