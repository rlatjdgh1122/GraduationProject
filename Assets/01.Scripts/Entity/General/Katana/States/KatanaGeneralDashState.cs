using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaGeneralDashState : KatanaGeneralBaseState
{
    public KatanaGeneralDashState(General penguin, EntityStateMachine<KatanaGeneralStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
