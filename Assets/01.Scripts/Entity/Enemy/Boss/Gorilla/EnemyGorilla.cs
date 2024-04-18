using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyGorillaStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Reached,
    MustChase,
    Provoked
}

public class EnemyGorilla : Enemy
{
    public EnemyStateMachine<EnemyGorillaStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyGorillaStateEnum>();

        foreach (EnemyGorillaStateEnum state in Enum.GetValues(typeof(EnemyGorillaStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyGorilla{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyGorillaStateEnum>;
            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(EnemyGorillaStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
