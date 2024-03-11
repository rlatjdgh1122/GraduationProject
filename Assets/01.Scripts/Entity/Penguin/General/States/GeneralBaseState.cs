using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GeneralBaseState : EntityState<GeneralPenguinStateEnum, General>
{
    public GeneralBaseState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Dead);
    }

    protected void HoldShield()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
