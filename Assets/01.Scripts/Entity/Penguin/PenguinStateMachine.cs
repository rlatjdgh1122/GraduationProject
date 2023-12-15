using Polyperfect.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStateMachine<T> where T : Enum
{
    public PenguinState<T> CurrentState { get; private set; }
    public PenguinState<T> PrevState { get; private set; }
    public Dictionary<T, PenguinState<T>> StateDictionary
        = new Dictionary<T, PenguinState<T>>();

    public void Init(T state)
    {
        CurrentState = StateDictionary[state];
        PrevState = CurrentState;
        CurrentState.Enter();
    }

    public void ChangeState(T newState)
    {
        PrevState = CurrentState;
        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(T state, PenguinState<T> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
