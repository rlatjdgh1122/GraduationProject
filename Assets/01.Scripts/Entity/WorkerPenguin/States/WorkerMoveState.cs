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
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_worker.CheckDistance() < 0.05f)
            _stateMachine.ChangeState(WorkerPenguinStateEnum.Work);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
