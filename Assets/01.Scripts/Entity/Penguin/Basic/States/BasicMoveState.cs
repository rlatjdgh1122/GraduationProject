using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveState : BasicBaseState
{
    public BasicMoveState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
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

        if (_penguin.IsInTargetRange) //타겟 안에 들어오면 && 클릭하지 않고 있을 때 -> Chase로
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

        if (_penguin.NavAgent.velocity.magnitude < 0.15f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
