using System;

public enum BuilderPenguinStateEnum
{
    Idle,
    Move,
    Work,
    Return
}

public class BuilderPenguin : Worker
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

    protected override void Start()
    {
        StateMachine.Init(WorkerPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
