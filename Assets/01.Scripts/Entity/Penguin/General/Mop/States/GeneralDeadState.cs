using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDeadState : GeneralBaseState
{
    public GeneralDeadState(Penguin penguin, PenguinStateMachine<GeneralPenguinStateEnum> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.CurrentTarget = null;
        _penguin.enabled = false;
        _penguin.NavAgent.enabled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }


}
