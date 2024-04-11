using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWaitState : WizardBaseState
{
    private float waitDuration = 3f;
    private float startTime;

    public WizardWaitState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Time.time - startTime >= waitDuration)
        {
            _stateMachine.ChangeState(WizardPenguinStateEnum.Attack);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
