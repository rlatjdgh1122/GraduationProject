using System;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStateMachine<T> where T : Enum
{
    public PenguinState<T> CurrentState { get; private set; }
    public PenguinState<T> PrevState { get; private set; }
    public Dictionary<T, PenguinState<T>> StateDictionary
        = new Dictionary<T, PenguinState<T>>();

    private Penguin _penguin;

    public void Init(T state)
    {
        CurrentState = StateDictionary[state];

        PrevState = CurrentState;
        CurrentState.Enter();
    }

    public void ChangeState(T newState)
    {
        PrevState = CurrentState;
        Debug.Log("���� ���� Ȯ�� : " +newState.ToString());
        Debug.Log("���� ���� Ȯ�� : " + PrevState.ToString());

        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void WaitingForCompletion(T newState, float value)
    {
        float time = _penguin.AnimatorCompo.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (time >= value)
        {
            ChangeState(newState);
        }
    }

    public void AddState(T state, PenguinState<T> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}
