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
    public EntityStateMachine<BasicPenguinStateEnum,Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<BasicPenguinStateEnum,Penguin>();

        foreach (BasicPenguinStateEnum state in Enum.GetValues(typeof(BasicPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Basic{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<BasicPenguinStateEnum,Penguin>;

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

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
