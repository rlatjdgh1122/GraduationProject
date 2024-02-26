using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMoveState : ShieldBaseState
{
    public ShieldMoveState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
