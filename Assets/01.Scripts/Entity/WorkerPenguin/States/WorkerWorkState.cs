using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerWorkState : WorkerState<WorkerPenguinStateEnum>
{
    public WorkerWorkState(Worker worker, WorkerStateMachine<WorkerPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(worker, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        Debug.Log("working");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _worker.LookTaget();

        if (_triggerCalled)
        {
            Debug.Log("Äµ´Ù");

            _triggerCalled = false;
        }

        if (_worker.EndWork)
            _stateMachine.ChangeState(WorkerPenguinStateEnum.Return);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
