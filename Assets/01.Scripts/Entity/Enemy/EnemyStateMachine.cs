using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState PrevState { get; private set; }
    public EnemyState CurrentState { get; private set; }
    public Dictionary<EnemyStateType, EnemyState> StateDictionary { get; private set; } = new();

    public void Init(EnemyStateType state)
    {
        PrevState = CurrentState;
        CurrentState = StateDictionary[state];
        CurrentState.EnterState();
    }

    public void ChangeState(EnemyStateType newState)
    {
        PrevState = CurrentState;
        CurrentState.ExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.EnterState();
    }

    public void AddState(EnemyStateType stateType, EnemyState playerState)
    {
        StateDictionary.Add(stateType, playerState);
    }
}
