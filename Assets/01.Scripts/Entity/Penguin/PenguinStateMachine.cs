using System;
using System.Collections.Generic;

public class PenguinStateMachine<T> where T : Enum
{
    private Penguin _owner = null;
    public EntityState<Enum, Penguin> CurrentState { get; private set; }
    public EntityState<Enum, Penguin> PrevState { get; private set; }

    // Enum�� Ű�� ����ϴ� Dictionary�� �����մϴ�.
    public Dictionary<Enum, EntityState<Enum, Penguin>> StateDictionary { get; } = new Dictionary<Enum, EntityState<Enum, Penguin>>();

    public void Setting(Penguin owner)
    {
        _owner = owner;
        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            // Enum Ÿ���� ������ �� ���¸� �����ɴϴ�.
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, this, typeName) as PenguinState<Enum, Penguin>;

            // StateDictionary�� ���¸� �߰��մϴ�.
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

         UnityEngine.Debug.Log($"���� : {PrevState}, ���� : {CurrentState}");
     }

     public void AddState(T state, EntityState<T, Penguin> playerState)
     {
         StateDictionary.Add(state, playerState);
     }*/

}
