using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMustMoveState : WizardBaseState
{
    public WizardMustMoveState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MustMoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.remainingDistance < 0.05f)
            {
                _stateMachine.ChangeState(WizardPenguinStateEnum.Idle);
            }
        }

        else if (_penguin.IsTargetInInnerRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(WizardPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
