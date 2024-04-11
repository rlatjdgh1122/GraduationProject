using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinIdleState : State
{
    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(PenguinStateType.Move);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(PenguinStateType.Chase);
    }

    public override void ExitState()
    {
        IdleExit();

        base.ExitState();
    }
}
