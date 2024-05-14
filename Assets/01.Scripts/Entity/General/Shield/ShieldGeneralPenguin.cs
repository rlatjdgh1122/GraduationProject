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

        var maxHp = HealthCompo.maxHealth;
        var curHp = HealthCompo.currentHealth;

        if (PenguinTriggerCalled == false
            && CheckHealthRatioPassive(maxHp, curHp, 90))
        {
            OnPassiveHealthRatioEvent();
        }
    }

    public override void OnPassiveHealthRatioEvent()
    {
        StateMachine.ChangeState(PenguinStateType.Impact);
        PenguinTriggerCalled = true;

        //spinattack Exit에서 PenguinTriggerCalled를 true로 바꿈
    }

    public override void OnPassiveHitEvent()
    {
        StateMachine.ChangeState(PenguinStateType.SpinAttack);
        //spinattack Enter에서 PenguinTriggerCalled를 false로 바꿈

        //idle Enter에서도 바꿈
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
