using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBaseState : PenguinState<MopGeneralPenguinStateEnum>
{
    public GeneralBaseState(Penguin penguin, PenguinStateMachine<MopGeneralPenguinStateEnum> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(MopGeneralPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
