using System;
public class AxeGeneralPenguin : Penguin
{
    public PenguinStateMachine<GeneralPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<GeneralPenguinStateEnum>();

        foreach (GeneralPenguinStateEnum state in Enum.GetValues(typeof(GeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Mop{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<GeneralPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }
}
