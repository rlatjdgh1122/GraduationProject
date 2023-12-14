using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherBaseState
{
    public ArcherIdleState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Move);

        if (_penguin.IsInTargetRange)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Move);
        else
            _penguin.FindNearestEnemy("Enemy");
    }

    public override void Exit()
    {
        base.Exit();
    }
}
