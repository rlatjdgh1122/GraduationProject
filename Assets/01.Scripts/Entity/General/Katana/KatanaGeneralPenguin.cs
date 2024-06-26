using System;
using UnityEngine;

public class KatanaGeneralPenguin : General
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

    public override void OnPassiveAttackEvent()
    {
        Debug.Log("잘되네요");
    }

    public override void OnSkillEvent()
    {
        StateMachine.ChangeState(PenguinStateType.KatanaSkill);
    }

    public override void OnUltimateEvent()
    {

    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}