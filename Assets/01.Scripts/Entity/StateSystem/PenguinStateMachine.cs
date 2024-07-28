using System;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStateMachine
{
    public State CurrentState { get; private set; }
    public State PrevState { get; private set; }

    private PenguinStateType prevStateType = PenguinStateType.MustMove;
    private PenguinStateType currentStateType = PenguinStateType.MustMove;

    public PenguinStateType StateType
    {
        get => currentStateType;
        set
        {
            if (currentStateType.Equals(value)) return;
            prevStateType = currentStateType;
            currentStateType = value;
        }
    }

    public Dictionary<PenguinStateType, State> StateDictionary { get; private set; }

    public PenguinStateMachine()
    {
        StateDictionary = new Dictionary<PenguinStateType, State>();
    }

    public void Init(PenguinStateType state)
    {
        StateType = state;

        PrevState = CurrentState;
        CurrentState = StateDictionary[state];
        CurrentState.EnterState();
    }

    public void ChangeState(PenguinStateType newState)
    {
        StateType = newState;

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

    public bool IsPrevState(PenguinStateType state)
    {
        return prevStateType.Equals(state);
    }
}
