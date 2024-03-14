using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ShieldPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Block,
    Impact,
    Stun,
    Dead
}

public class ShieldPenguin : Penguin
{
    public EntityStateMachine<ShieldPenguinStateEnum, Penguin> StateMachine { get; private set; }
      
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<ShieldPenguinStateEnum, Penguin>();

        foreach (ShieldPenguinStateEnum state in Enum.GetValues(typeof(ShieldPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Shield{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ShieldPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(ShieldPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveStunEvent()
    {
        StateMachine.ChangeState(ShieldPenguinStateEnum.Stun);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}