using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMoveState : PenguinState
{
    public PenguinMoveState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.Input.ClickEvent += HandleClick;
        _penguin.SetMovement();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.remainingDistance < 0.1f && !_penguin.NavAgent.pathPending)
        {
            _stateMachine.ChangeState(PenguinStateEnum.Idle); //목적지에 도달했을 때 Idle상태로 바꿔준다.
        }
    }

    private void HandleClick()
    {
        _stateMachine.ChangeState(PenguinStateEnum.Move);
    }

    public override void Exit()
    {
        _penguin.Input.ClickEvent -= HandleClick;
        base.Exit();
    }
}
