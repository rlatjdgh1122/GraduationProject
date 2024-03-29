using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMustMoveState : ArcherBaseState
{
    public ArcherMustMoveState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);
            }
        }

        else if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
