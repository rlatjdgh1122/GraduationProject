using System;
using System.Collections.Generic;

public class EntityStateMachine<T, G> where T : Enum where G : Entity
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

        UnityEngine.Debug.Log("이 전 : " + PrevState + " : 지금 : " + CurrentState);
    }

    public void AddState(T state, EntityState<T, G> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
