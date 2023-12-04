using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinDeadState : PenguinBaseState
{
    public PenguinDeadState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.tag = "Untagged";
        _penguin.Target.FindNearestPenguin("Enemy");
        _penguin.CharController.enabled = false;
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
