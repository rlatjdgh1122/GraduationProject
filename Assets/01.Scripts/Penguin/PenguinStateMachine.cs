using System.Collections.Generic;
using UnityEngine;

public enum PenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack
}

public class PenguinStateMachine : MonoBehaviour
{
    public PenguinState CurrentState { get; private set; }
    public Dictionary<PenguinStateEnum, PenguinState> StateDictionary
        = new Dictionary<PenguinStateEnum, PenguinState>();

    private Penguin _penguin;

    public void Init(PenguinStateEnum state, Penguin penguin)
    {
        _penguin = penguin;
        CurrentState = StateDictionary[state];
        CurrentState.Enter();
    }

    public void ChangeState(PenguinStateEnum newState)
    {
        CurrentState.Exit();
        Debug.Log(newState);
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void WaitingForCompletion(PenguinStateEnum newState, float value)
    {
        float time = _penguin.AnimatorCompo.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (time >= value)
        {
            ChangeState(newState);
        }
    }

    public void AddState(PenguinStateEnum state, PenguinState playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
