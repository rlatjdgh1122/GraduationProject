using Polyperfect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PenguinIdleState : PenguinBaseState
{
    public PenguinIdleState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
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

        if (_penguin.IsInside && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
