using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine<T> where T : Enum
{
    public EnemyState<T> PrevState { get; private set; }
    public EnemyState<T> CurrentState { get; private set; }
    public Dictionary<T, EnemyState<T>> StateDictionary = new Dictionary<T, EnemyState<T>>();

    public void Init(T startState)
    {
        Debug.Log(Equals(typeof(T), startState.GetType()));

        PrevState = CurrentState;
        CurrentState = StateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(T newState)
    {
        PrevState = CurrentState;
        PrevState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();

        //UnityEngine.Debug.Log("이 전 : " + PrevState + " , 이 후 : " + CurrentState);
    }

    public bool IsEqualPrevState(T state)
        => PrevState.Equals(state);

    public void AddState(T state, EnemyState<T> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
