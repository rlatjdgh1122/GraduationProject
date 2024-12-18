using System;
using UnityEngine;

public enum WorkerPenguinStateEnum
{
    Idle,
    Move,
    Work,
    Return
}

public class MinerPenguin : Worker
{
    public WorkerStateMachine<WorkerPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new WorkerStateMachine<WorkerPenguinStateEnum>();

        foreach (WorkerPenguinStateEnum state in Enum.GetValues(typeof(WorkerPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Worker{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as WorkerState<WorkerPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(WorkerPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        if(WorkerStateCheck)
        {
            StateMachine.Init(WorkerPenguinStateEnum.Idle);
            WorkerStateCheck = false;
        }

        StateMachine.CurrentState.UpdateState();
    }

    public override void Init()
    {
        CanWork = false;
        EndWork = false;
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
