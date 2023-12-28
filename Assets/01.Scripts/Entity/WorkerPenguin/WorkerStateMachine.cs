using System;
using System.Collections.Generic;

public class WorkerStateMachine<T> where T : Enum
{
    public WorkerState<T> CurrentState { get; private set; }
    public WorkerState<T> PrevState { get; private set; }
    public Dictionary<T, WorkerState<T>> StateDictionary
        = new Dictionary<T, WorkerState<T>>();

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

    public void AddState(T state, WorkerState<T> workerState)
    {
        StateDictionary.Add(state, workerState);
    }
}
