using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaGeneralIdleState : KatanaGeneralBaseState
{
    public KatanaGeneralIdleState(General penguin, EntityStateMachine<KatanaGeneralStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(KatanaGeneralStateEnum.Move);

        if (_penguin.IsTargetInInnerRange)
            _stateMachine.ChangeState(KatanaGeneralStateEnum.Chase);
    }

    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
