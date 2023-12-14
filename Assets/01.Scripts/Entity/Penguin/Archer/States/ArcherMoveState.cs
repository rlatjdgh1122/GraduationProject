using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherBaseState
{
    public ArcherMoveState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);

        if (_penguin.IsInTargetRange)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
