using System;
using UnityEngine;

public enum BasicPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
}

public class MeleePenguin : Penguin
{

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        StateInit();
    }
    public override void StateInit()
    {
        StateMachine.Init(PenguinStateType.Idle);
    }
    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
