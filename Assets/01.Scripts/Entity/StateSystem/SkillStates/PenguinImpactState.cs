using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinImpactState : State
{

    public int HitCount = 0;

    public PenguinImpactState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _triggerCalled = false;
        _penguin.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Block);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        if (_penguin.CheckHitPassive(++HitCount))
        {
            _penguin?.OnPassiveHitEvent();

            HitCount = 0;
        }
    }

}
