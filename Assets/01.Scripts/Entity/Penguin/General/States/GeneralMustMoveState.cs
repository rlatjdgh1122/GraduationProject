using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMustMoveState : GeneralBaseState
{
    public GeneralMustMoveState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
            }
        }

        if (_penguin.IsInnerTargetRange
             && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
