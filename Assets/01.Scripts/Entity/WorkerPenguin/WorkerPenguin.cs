using System;

public enum WorkerPenguinStateEnum
{
    Move,
    Work,
    Return
}

public class WorkerPenguin : Worker
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
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as WorkerState<WorkerPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected void Start()
    {
        StateMachine.Init(WorkerPenguinStateEnum.Move);
    }

    protected void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
