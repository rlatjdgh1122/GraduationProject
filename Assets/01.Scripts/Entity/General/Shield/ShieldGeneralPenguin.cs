using System;
using UnityEngine;

public class ShieldGeneralPenguin : General
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
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }
    public override void StateInit()
    {
        StateMachine.Init(PenguinStateType.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
