using System;
using UnityEngine;

public enum ArcherPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
    Dead
}

public class ArcherPenguin : Penguin
{
    public EntityStateMachine<ArcherPenguinStateEnum, Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<ArcherPenguinStateEnum, Penguin>();

        foreach (ArcherPenguinStateEnum state in Enum.GetValues(typeof(ArcherPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Archer{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ArcherPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }
    public override void StateInit()
    {
        StateMachine.Init(ArcherPenguinStateEnum.Idle);
    }
    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
