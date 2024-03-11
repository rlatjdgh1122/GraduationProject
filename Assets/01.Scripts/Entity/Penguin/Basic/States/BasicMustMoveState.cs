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

        Debug.Log(_penguin.transform.position + " : " + _penguin.GetSeatPosition());

    }
    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.MoveToTarget(_penguin.GetSeatPosition());

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            {
                Debug.Log("MustMove : " + _penguin.NavAgent.velocity.magnitude);
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            }
        }

        if (_penguin.IsInnerTargetRange
             && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
