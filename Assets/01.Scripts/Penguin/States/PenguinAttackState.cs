using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttackState : PenguinBaseState
{
    public PenguinAttackState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
