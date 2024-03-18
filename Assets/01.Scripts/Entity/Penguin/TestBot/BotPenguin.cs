using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BotPenguinStateEnum
{
    Idle,
    Chase,
    Attack,
    Dead,
    Provoked,
}

public class BotPenguin : Penguin
{
    public EnemyStateMachine<BotPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<BotPenguinStateEnum>();

        foreach (BotPenguinStateEnum state in Enum.GetValues(typeof(BotPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Bot{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<BotPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(BotPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
