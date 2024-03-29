using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherBaseState
{
    public ArcherMoveState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);
        }

        else if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
