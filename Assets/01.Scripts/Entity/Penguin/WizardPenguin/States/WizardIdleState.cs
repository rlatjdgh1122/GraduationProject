using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardIdleState : WizardBaseState
{
    public WizardIdleState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
        {
            _stateMachine.ChangeState(WizardPenguinStateEnum.Move);
        }

        else if (_penguin.IsTargetInInnerRange)
            _stateMachine.ChangeState(WizardPenguinStateEnum.Chase);
    }


    public override void Exit()
    {
        
        IdleExit();

        base.Exit();
    }
}
