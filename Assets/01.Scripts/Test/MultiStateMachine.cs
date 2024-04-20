
#region 필요하면 제작할 것
using System;
using System.Collections.Generic;

public enum DefaultState
{
    Idle,
}

public class MultiStateMachine<T> where T : Enum
{
    public EnemyState<T> PrevState { get; private set; }
    public EnemyState<DefaultState> DefaultPrevState { get; private set; }
    public EnemyState<T> CurrentState { get; private set; }
    public EnemyState<DefaultState> DefaultCurrentState { get; private set; }

    public Dictionary<T, EnemyState<T>> StateDictionary = new();
    public Dictionary<DefaultState, EnemyState<DefaultState>> DefaultStateDictionary;

    public void Setting()
    {
        DefaultStateDictionary = new();

        foreach (DefaultState state in Enum.GetValues(typeof(DefaultState)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Default{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, this, typeName) as EnemyState<DefaultState>;
            DefaultStateDictionary.Add(state, newState);
        }
    }
    public void Init(T startState)
    {
        PrevState = CurrentState;
        CurrentState = StateDictionary[startState];
        CurrentState.Enter();
    }
    public void Init(DefaultState startState)
    {
        DefaultPrevState = DefaultCurrentState;

        DefaultCurrentState = DefaultStateDictionary[startState];
        DefaultCurrentState.Enter();
    }


    public void ChangeState(T newState)
    {
        if (Enum.Equals(typeof(DefaultState), newState.GetType()))
        {

        }
        PrevState = CurrentState;
        PrevState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }
    public void ChangeState(DefaultState newState)
    {
        PrevState = CurrentState;
        PrevState.Exit();

        DefaultCurrentState = DefaultStateDictionary[newState];
        DefaultCurrentState.Enter();
    }

    public Enum PrevEnum(Type type)
    {
        if (type == typeof(DefaultState))
        {
            return default(DefaultState);
        }
        else
            return default(T);
    }

    public bool IsEqualPrevState(T state)
        => PrevState.Equals(state);

    public void AddState(T state, EnemyState<T> playerState)
    {
        StateDictionary.Add(state, playerState);
    }
}

#endregion