using System;
using UnityEngine;
using UnityEngine.Events;

public class LanceGeneralPenguin : General
{
    protected override void Awake()
    {
        base.Awake();

        SetBaseState();
    }

    protected override void Start()
    {
        base.Start();
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

    public override void OnPassiveHealthRatioEvent()
    {
        
    }

    public override void OnPassiveAttackEvent()
    {

    }

    public override void OnSkillEvent()
    {
        StateMachine.ChangeState(PenguinStateType.LanceSkill);
    }

    public override void OnUltimateEvent()
    {
        StateMachine.ChangeState(PenguinStateType.LanceUltimate);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
