using UnityEngine;
using UnityEngine.Events;

public class ShieldGeneralPenguin : General
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

    protected override void HandleHit()
    {
        base.HandleHit();
    }

    public override void OnSkillEvent()
    {
        StateMachine.ChangeState(PenguinStateType.SpinAttack);
    }

    public override void OnUltimateEvent()
    {
        StateMachine.ChangeState(PenguinStateType.ShieldUltimate);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
