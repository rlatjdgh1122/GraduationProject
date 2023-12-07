using System;
using UnityEngine;

public enum ArcherPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead
}

public class ArcherPenguin : Penguin
{
    public PenguinStateMachine<ArcherPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine<ArcherPenguinStateEnum>();

        foreach (ArcherPenguinStateEnum state in Enum.GetValues(typeof(ArcherPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Archer{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState<ArcherPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(ArcherPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}