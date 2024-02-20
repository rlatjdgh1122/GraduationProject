using System;

public enum MopGeneralPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    AoEAttack, //광격공격
    Dead
}

public class MopGeneralPenguin : Penguin
{
    public PenguinStateMachine<MopGeneralPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<MopGeneralPenguinStateEnum>();

        foreach (MopGeneralPenguinStateEnum state in Enum.GetValues(typeof(MopGeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Mop{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<MopGeneralPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(MopGeneralPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
