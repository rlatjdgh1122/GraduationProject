using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBaseState : PenguinState<WizardPenguinStateEnum, Penguin>
{
    public WizardBaseState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(WizardPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
