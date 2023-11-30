using System.Collections.Generic;

public enum PenguinStateEnum
{
    Idle,
    Move,
}

public class PenguinStateMachine
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
