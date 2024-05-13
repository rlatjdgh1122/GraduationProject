using System;
using UnityEngine;

public class ShieldGeneralPenguin : General
{

    private bool IsStateImpact = false;

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

        if (IsStateImpact == false
            && CheckHealthRatioPassive(maxHp, curHp, 90))
        {
            OnPassiveHealthRatioEvent();
        }
    }

    public override void OnPassiveHealthRatioEvent()
    {
        _characterStat.armor.AddIncrease(500);
        StateMachine.ChangeState(PenguinStateType.Impact);
        Debug.Log("방어모드");
        IsStateImpact = true;
    }

    public override void OnPassiveHitEvent()
    {
        StateMachine.ChangeState(PenguinStateType.SpinAttack);
        IsStateImpact = false;
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
