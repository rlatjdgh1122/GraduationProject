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
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ShieldPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }
    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void StateInit()
    {
        StateMachine.Init(ShieldPenguinStateEnum.Idle);
    }

    public override void OnPassiveStunEvent()
    {
        StateMachine.ChangeState(ShieldPenguinStateEnum.Stun);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}