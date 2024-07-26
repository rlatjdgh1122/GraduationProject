using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinIdleState : State
{
    public PenguinIdleState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

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

        CheckCommandModeForMovement();
        CheckBattleModeForChase();
    }


    public override void ExitState()
    {
        base.ExitState();

        IdleExit();
    }
}
