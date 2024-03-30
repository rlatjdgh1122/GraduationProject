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

        _worker.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _worker.LookTaget();

        if (_worker.EndWork || WaveManager.Instance.IsBattlePhase)
        {
            if (_triggerCalled)
            {
                _worker.CanWork = false;
                _worker.EndWork = true;
                _stateMachine.ChangeState(WorkerPenguinStateEnum.Return);
            }
        }

    }

    public override void Exit()
    {
        _worker.StartImmediately();

        base.Exit();
    }
}
