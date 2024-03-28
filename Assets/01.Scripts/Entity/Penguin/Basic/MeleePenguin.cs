using System;
using UnityEngine;

public enum BasicPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
    Dead,
    FreelyMove,
}

public class MeleePenguin : Penguin
{
    public EntityStateMachine<BasicPenguinStateEnum, Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<BasicPenguinStateEnum, Penguin>();

        foreach (BasicPenguinStateEnum state in Enum.GetValues(typeof(BasicPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Basic{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<BasicPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }
    protected override void Start()
    {
        StateInit();
    }
    public override void StateInit()
    {
        StateMachine.Init(BasicPenguinStateEnum.Idle);
    }
    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
