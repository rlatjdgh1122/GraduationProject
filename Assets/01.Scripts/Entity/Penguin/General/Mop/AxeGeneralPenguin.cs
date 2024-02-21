using System;

public enum AxeGeneralPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead
}
public class AxeGeneralPenguin : Penguin
{
    public PenguinStateMachine<AxeGeneralPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<AxeGeneralPenguinStateEnum>();

        foreach (AxeGeneralPenguinStateEnum state in Enum.GetValues(typeof(AxeGeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Mop{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<AxeGeneralPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }
}
