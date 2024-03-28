using System;
using System.Collections.Generic;

public class PenguinStateMachine<T> where T : Enum
{
    private Penguin _owner = null;
    public EntityState<Enum, Penguin> CurrentState { get; private set; }
    public EntityState<Enum, Penguin> PrevState { get; private set; }

    // Enum을 키로 사용하는 Dictionary를 선언합니다.
    public Dictionary<Enum, EntityState<Enum, Penguin>> StateDictionary { get; } = new Dictionary<Enum, EntityState<Enum, Penguin>>();

    public void Setting(Penguin owner)
    {
        _owner = owner;
        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            // Enum 타입의 값으로 각 상태를 가져옵니다.
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, this, typeName) as PenguinState<Enum, Penguin>;

            // StateDictionary에 상태를 추가합니다.
            StateDictionary.Add(state, newState);
        }
    }
    /* public void Init(T state)
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

         UnityEngine.Debug.Log($"이전 : {PrevState}, 이후 : {CurrentState}");
     }

     public void AddState(T state, EntityState<T, Penguin> playerState)
     {
         StateDictionary.Add(state, playerState);
     }*/

}
