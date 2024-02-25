using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBaseState : PenguinState<GeneralPenguinStateEnum>
{
    public GeneralBaseState(Penguin penguin, PenguinStateMachine<GeneralPenguinStateEnum> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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

    public override void Exit()
    {
        base.Exit();
    }

}
