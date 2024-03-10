using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMustMoveState : BasicBaseState
{
    public BasicMustMoveState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _penguin.MoveToTarget(_penguin.GetSeatPosition());
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
