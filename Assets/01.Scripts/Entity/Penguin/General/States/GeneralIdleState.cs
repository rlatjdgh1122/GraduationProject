using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralIdleState : GeneralBaseState
{
    public GeneralIdleState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Move);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }



}