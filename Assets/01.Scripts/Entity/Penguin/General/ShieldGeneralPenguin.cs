using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralPenguin : General
{
    public EntityStateMachine<GeneralPenguinStateEnum, General> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<GeneralPenguinStateEnum, General>();

        foreach (GeneralPenguinStateEnum state in Enum.GetValues(typeof(GeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"General{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<GeneralPenguinStateEnum, General>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdateState();
    }
    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }
    public override void StateInit()
    {
        StateMachine.Init(GeneralPenguinStateEnum.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
