using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopMoveState : MopBaseState
{
    public MopMoveState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
        }

        else if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
