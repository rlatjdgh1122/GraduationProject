using System;
using UnityEngine;

public enum BasicPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead
}

public class BasicPenguin : Penguin
{
    public PenguinStateMachine<BasicPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<BasicPenguinStateEnum>();

        foreach (BasicPenguinStateEnum state in Enum.GetValues(typeof(BasicPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Penguin{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<BasicPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(BasicPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
