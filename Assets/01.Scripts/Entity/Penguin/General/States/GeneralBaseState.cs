using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GeneralBaseState : GeneralState<GeneralPenguinStateEnum, General>
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
        if (_penguin.skill.IsAvaliable)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
