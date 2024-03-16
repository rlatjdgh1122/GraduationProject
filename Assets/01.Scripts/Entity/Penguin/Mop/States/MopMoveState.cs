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

        _triggerCalled = true;
        _penguin.SuccessfulToArmyCalled = false;

        MoveEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Move Update: " + _penguin.NavAgent.velocity.magnitude);
        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            Debug.Log("Move : º¯°æ");
            _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
        }

        if (_penguin.IsInnerTargetRange
             && _penguin.MoveFocusMode == MovefocusMode.Battle)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
