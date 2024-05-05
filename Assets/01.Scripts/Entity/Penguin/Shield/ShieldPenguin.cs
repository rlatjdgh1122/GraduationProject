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
    public EntityStateMachine<ShieldPenguinStateEnum, Penguin> _stateMachine { get; private set; }
      
    protected override void Awake()
    {
        base.Awake();

        _stateMachine = new EntityStateMachine<ShieldPenguinStateEnum, Penguin>();

        foreach (ShieldPenguinStateEnum state in Enum.GetValues(typeof(ShieldPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Shield{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, _stateMachine, typeName) as EntityState<ShieldPenguinStateEnum, Penguin>;

            _stateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }
    protected override void Update()
    {
        _stateMachine.CurrentState.UpdateState();
    }

    public override void StateInit()
    {
        _stateMachine.Init(ShieldPenguinStateEnum.Idle);
    }

    public override void OnPassiveStunEvent()
    {
        _stateMachine.ChangeState(ShieldPenguinStateEnum.Stun);
    }

    public override void AnimationTrigger() => _stateMachine.CurrentState.AnimationFinishTrigger();
}