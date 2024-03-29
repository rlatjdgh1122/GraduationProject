using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeadState : ArcherBaseState
{
    public ArcherDeadState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
