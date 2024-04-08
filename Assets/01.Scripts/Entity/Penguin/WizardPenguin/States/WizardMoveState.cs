using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMoveState : WizardBaseState
{
    public WizardMoveState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        MoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(WizardPenguinStateEnum.Idle);
        }

        else if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(WizardPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
