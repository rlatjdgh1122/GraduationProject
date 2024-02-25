using System;
public class MopGeneralPenguin : Penguin
{
    public PenguinStateMachine<GeneralPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<GeneralPenguinStateEnum>();

        foreach (GeneralPenguinStateEnum state in Enum.GetValues(typeof(GeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"General{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<GeneralPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(GeneralPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(GeneralPenguinStateEnum.AoEAttack);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
