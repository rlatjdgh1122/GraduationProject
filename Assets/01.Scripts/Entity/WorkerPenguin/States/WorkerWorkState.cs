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
            if (_worker.EndWork || WaveManager.Instance.IsBattlePhase)
            {
                _worker.CanWork = false;
                _worker.EndWork = true;
                _stateMachine.ChangeState(WorkerPenguinStateEnum.Return);
            }
              

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
