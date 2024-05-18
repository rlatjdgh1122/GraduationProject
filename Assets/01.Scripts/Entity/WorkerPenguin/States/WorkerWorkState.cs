using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
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
        else
        {
            if (!_worker.IsTargetInAttackRange)
                _stateMachine.ChangeState(WorkerPenguinStateEnum.Move);
        }

    }

    public override void Exit()
    {
        _worker.StartImmediately();
        base.Exit();
    }
}
