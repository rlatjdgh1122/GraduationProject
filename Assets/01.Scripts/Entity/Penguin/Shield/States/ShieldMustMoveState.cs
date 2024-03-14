using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMustMoveState : ShieldBaseState
{
    public ShieldMustMoveState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _penguin.StartImmediately();
        _penguin.MoveToPosition(_penguin.GetSeatPosition());
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            {
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
            }
        }

        if (_penguin.IsInnerTargetRange
             && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
