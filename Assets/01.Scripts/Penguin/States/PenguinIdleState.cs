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
        _penguin.Input.ClickEvent += HandleClick;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleClick()
    {
        _stateMachine.ChangeState(PenguinStateEnum.Move);
    }

    public override void Exit()
    {
        _penguin.Input.ClickEvent -= HandleClick;
        base.Exit();
    }
}
