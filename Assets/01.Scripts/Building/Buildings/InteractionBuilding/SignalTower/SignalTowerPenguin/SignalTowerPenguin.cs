using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalTowerPenguinStateEnum
{
    Idle,
    Watch,
    Dead
}

public class SignalTowerPenguin : Penguin
{
    public EntityStateMachine<SignalTowerPenguinStateEnum, Penguin> StateMachine {  get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<SignalTowerPenguinStateEnum, Penguin>();

        foreach (SignalTowerPenguinStateEnum state in Enum.GetValues(typeof(SignalTowerPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"SignalTower{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<SignalTowerPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }
    protected override void Start()
    {
        StateMachine.Init(SignalTowerPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void HandleDie()
    {

    }
}
