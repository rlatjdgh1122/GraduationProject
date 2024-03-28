using System;
using UnityEngine;

public enum MopPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
    AoEAttack,
    FreelyMove,
    Dead
}

public class MopPenguin : Penguin
{
    public EntityStateMachine<MopPenguinStateEnum, Penguin> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<MopPenguinStateEnum, Penguin>();

        foreach (MopPenguinStateEnum state in Enum.GetValues(typeof(MopPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Mop{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<MopPenguinStateEnum, Penguin>;

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
        StateMachine.Init(MopPenguinStateEnum.Idle);
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(MopPenguinStateEnum.AoEAttack);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
