using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttackState : PenguinState
{
    public PenguinAttackState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) 
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}
