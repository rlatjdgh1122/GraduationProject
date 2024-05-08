using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStateMachine
{
    public DummyState CurrentState { get; private set; }
    public DummyState PrevState { get; private set; }

    public Dictionary<DummyPenguinStateEnum, DummyState> StateDictionary
        = new Dictionary<DummyPenguinStateEnum, DummyState>();

    public void Init(DummyPenguinStateEnum state)
    {
        CurrentState = StateDictionary[state];
        PrevState = CurrentState;
        CurrentState.Enter();
    }
    public void ChangeState(DummyPenguinStateEnum newState)
    {
        PrevState = CurrentState;
        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();

        //Debug.Log($"이전 : {PrevState}, 이후 : {CurrentState}");
    }

    public void AddState(DummyPenguinStateEnum state, DummyState playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
