using System.Collections.Generic;
using UnityEngine;

public class PenguinStateMachine
{
    public State CurrentState { get; private set; }
    public State PrevState { get; private set; }

    public Dictionary<PenguinStateType, State> StateDictionary { get; private set; }

    public PenguinStateMachine()
    {
        StateDictionary = new Dictionary<PenguinStateType, State>();
    }

    public void Init(PenguinStateType state)
    {
        PrevState = CurrentState;
        CurrentState = StateDictionary[state];
        CurrentState.EnterState();
    }

    public void ChangeState(PenguinStateType newState)
    {
        PrevState = CurrentState;
        CurrentState.ExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.EnterState();

        //Debug.Log($"{PrevState} -> {CurrentState}");
    }

    public void AddState(PenguinStateType stateType, State playerState)
    {
        StateDictionary.Add(stateType, playerState);
    }
}
