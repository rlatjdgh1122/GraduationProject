using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArcherTowerPenguinStateEnum
{
    Idle,
    Attack,
    Dead
}


public class ArcherTowerPenguin : Penguin  
{
    public EntityStateMachine <ArcherTowerPenguinStateEnum, Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin>();

        foreach (ArcherTowerPenguinStateEnum state in Enum.GetValues(typeof(ArcherTowerPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"ArcherTower{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ArcherTowerPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(ArcherTowerPenguinStateEnum.Idle);
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
