using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralImpactState : ShieldGeneralBaseState
{
    public ShieldGeneralImpactState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _triggerCalled = false;
        _penguin.StopImmediately();

        _penguin.skill.OnSkillCompleted += SpinAttack;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Block);
        }
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.SpinAttack);
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillCompleted -= SpinAttack;
        base.Exit();
        _triggerCalled = true;
    }
}
