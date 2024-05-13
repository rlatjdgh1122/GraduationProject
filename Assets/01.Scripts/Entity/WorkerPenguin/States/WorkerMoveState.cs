using UnityEngine.AI;

public class WorkerMoveState : WorkerState<WorkerPenguinStateEnum>
{
    public WorkerMoveState(Worker worker, WorkerStateMachine<WorkerPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(worker, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        _worker.MoveToTarget();

        _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_worker.IsTargetInAttackRange)
            _stateMachine.ChangeState(WorkerPenguinStateEnum.Work);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
