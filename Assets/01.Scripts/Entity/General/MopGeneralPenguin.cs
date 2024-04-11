using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MopGeneralPenguin : General
{
    public EntityStateMachine<ShieldGeneralPenguinStateEnum,General> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<ShieldGeneralPenguinStateEnum,General>();

        foreach (ShieldGeneralPenguinStateEnum state in Enum.GetValues(typeof(ShieldGeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"General{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ShieldGeneralPenguinStateEnum, General>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(ShieldGeneralPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(ShieldGeneralPenguinStateEnum.AoEAttack);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
