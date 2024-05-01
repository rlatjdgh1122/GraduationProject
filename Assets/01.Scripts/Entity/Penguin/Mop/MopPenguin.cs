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
}

public class MopPenguin : Penguin
{
    protected override void Awake()
    {
        base.Awake();
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
        StateMachine.Init(PenguinStateType.Idle);
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(PenguinStateType.AoEAttack);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
