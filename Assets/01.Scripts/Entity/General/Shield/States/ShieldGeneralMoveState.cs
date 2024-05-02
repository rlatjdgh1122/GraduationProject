using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralMoveState : ShieldGeneralBaseState
{
    public ShieldGeneralMoveState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Idle);

        if (_penguin.IsTargetInInnerRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
