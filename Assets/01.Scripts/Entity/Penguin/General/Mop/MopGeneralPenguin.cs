using System;

public enum MopGeneralPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    AoEAttack, //���ݰ���
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
            //���÷���
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
