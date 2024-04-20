using System;
using Unity.VisualScripting;
using UnityEngine;

public class KatanaGeneralPenguin : General
{
    public PenguinStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine();

        foreach (PenguinStateType state in Enum.GetValues(typeof(PenguinStateType)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Penguin{typeName}State");
            //리플렉션
            State newState = Activator.CreateInstance(t, this, StateMachine, typeName) as State;
            if (newState == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            //newState.SetUp(this, StateMachine, typeName);
            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }

    public override void StateInit()
    {
        StateMachine.Init(PenguinStateType.Idle);
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(PenguinStateType.Dash);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
