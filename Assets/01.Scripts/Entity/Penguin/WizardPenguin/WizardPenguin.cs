using System;
using UnityEngine;

public enum WizardPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
    Dead
}

public class WizardPenguin : Penguin
{
    public EntityStateMachine<WizardPenguinStateEnum, Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<WizardPenguinStateEnum, Penguin>();

        foreach (WizardPenguinStateEnum state in Enum.GetValues(typeof(WizardPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Wizard{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<WizardPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }
    public override void StateInit()
    {
        StateMachine.Init(WizardPenguinStateEnum.Idle);
    }
    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
