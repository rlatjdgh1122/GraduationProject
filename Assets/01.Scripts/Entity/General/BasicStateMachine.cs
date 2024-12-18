using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateMachine<T, G> where T : Enum where G : Entity
{
    public EntityState<T, G> CurrentState { get; private set; }
    public EntityState<T, G> PrevState { get; private set; }

    public Dictionary<T, EntityState<T, G>> StateDictionary
        = new Dictionary<T, EntityState<T, G>>();

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

        //UnityEngine.Debug.Log($"이전 : {PrevState}, 이후 : {CurrentState}");
    }

    /// <summary>
    /// 만약 이전 상태가 파라미터 값과 같다면
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsEqualPrevState(T state)
    => PrevState.Equals(state);

    public void AddState(T state, EntityState<T, G> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
