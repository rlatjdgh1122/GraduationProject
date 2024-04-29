using System;
using UnityEngine;

public enum ArcherPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
}

public class ArcherPenguin : Penguin
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
