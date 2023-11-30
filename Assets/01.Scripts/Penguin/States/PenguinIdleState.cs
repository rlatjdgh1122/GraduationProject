using Polyperfect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinIdleState : PenguinState
{
    public PenguinIdleState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsInside && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(PenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
