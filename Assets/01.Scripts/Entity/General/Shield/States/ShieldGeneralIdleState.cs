using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralIdleState : ShieldGeneralBaseState
{
    public ShieldGeneralIdleState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Move);

        if (_penguin.IsTargetInInnerRange)
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
