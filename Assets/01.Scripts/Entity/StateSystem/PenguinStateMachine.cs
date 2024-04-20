using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStateMachine
{
    public IState CurrentState { get; private set; }
    public IState PrevState { get; private set; }

    public Dictionary<PenguinStateType, IState> StateDictionary { get; private set; }

    public PenguinStateMachine()
    {
        StateDictionary = new Dictionary<PenguinStateType, IState>();
    }

    public void Init(PenguinStateType state)
    {
        PrevState = CurrentState;
        CurrentState = StateDictionary[state];
        CurrentState.EnterState();
    }

    public void ChangeState(PenguinStateType newState)
    {
        Debug.Log(newState);
        PrevState = CurrentState;
        CurrentState.ExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.EnterState();
    }

    public void AddState(PenguinStateType stateType, IState playerState)
    {
        StateDictionary.Add(stateType, playerState);
    }
}
