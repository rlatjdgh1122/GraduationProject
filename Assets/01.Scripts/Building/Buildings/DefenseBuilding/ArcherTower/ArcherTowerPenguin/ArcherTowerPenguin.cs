using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArcherTowerPenguinStateEnum
{
    Idle,
    Attack,
}


public class ArcherTowerPenguin : Penguin  
{
    public EntityStateMachine <ArcherTowerPenguinStateEnum, Penguin> _StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _StateMachine = new EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin>();

        foreach (ArcherTowerPenguinStateEnum state in Enum.GetValues(typeof(ArcherTowerPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"ArcherTower{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, _StateMachine, typeName) as EntityState<ArcherTowerPenguinStateEnum, Penguin>;

            _StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        _StateMachine.Init(ArcherTowerPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        _StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => _StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void HandleDie()
    {

    }
}
