using ArmySystem;
using System;
using UnityEngine;

public class PenguinMoveState : State
{
    public PenguinMoveState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        CheckBattleModeForChase();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(PenguinStateType.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}