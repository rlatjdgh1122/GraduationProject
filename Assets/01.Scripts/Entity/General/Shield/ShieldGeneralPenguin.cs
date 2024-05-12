using System;
using UnityEngine;

public class ShieldGeneralPenguin : General
{
    protected override void Awake()
    {
        base.Awake();

        SetBaseState();
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

    protected override void HandleHit()
    {
        base.HandleHit();

        if(CheckHealthRatioPassive(HealthCompo.maxHealth, HealthCompo.currentHealth, 90))
        {
            OnPassiveHealthRatioEvent();
        }
    }

    public override void OnPassiveHealthRatioEvent()
    {
        StateMachine.ChangeState(PenguinStateType.Impact);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
