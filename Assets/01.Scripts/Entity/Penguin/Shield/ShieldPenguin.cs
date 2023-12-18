using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ShieldPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Block,
    Impact,
    Dead
}

public class SheildPenguin : Penguin
{
    public PenguinStateMachine<ShieldPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<ShieldPenguinStateEnum>();

        foreach (ShieldPenguinStateEnum state in Enum.GetValues(typeof(ShieldPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Shield{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<ShieldPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(ShieldPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}