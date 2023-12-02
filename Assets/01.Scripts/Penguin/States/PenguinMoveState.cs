using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMoveState : PenguinBaseState
{
    public PenguinMoveState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInside && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

        if (_penguin.NavAgent.remainingDistance < 0.1f && !_penguin.NavAgent.pathPending)
        {
            if (_penguin.IsClickToMoving)
                _penguin.IsClickToMoving = false;
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle); //목적지에 도달했을 때 Idle상태로 바꿔준다.
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
