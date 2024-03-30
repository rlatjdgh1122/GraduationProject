using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMoveState : GeneralBaseState
{
    public GeneralMoveState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);

        if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
