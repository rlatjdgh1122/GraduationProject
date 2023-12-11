using Polyperfect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class BasicIdleState : BasicBaseState
{
    public BasicIdleState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInTargetRange && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
