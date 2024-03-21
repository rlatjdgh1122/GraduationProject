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

       MustMoveEnter();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            
            if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            }
        }
        if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();

       
    }

    public override void Exit()
    {
        base.Exit();
    }


}
