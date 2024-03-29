using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDeadState : GeneralBaseState
{
    public GeneralDeadState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        DeadEnter();
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
