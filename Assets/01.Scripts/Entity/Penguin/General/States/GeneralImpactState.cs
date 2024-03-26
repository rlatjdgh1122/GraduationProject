using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralImpactState : GeneralBaseState
{
    public GeneralImpactState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;

        _penguin.skill.OnSkillCompleted += SpinAttack;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled && !_penguin.IsDead)
        {
            if (_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);
            else
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);
        }
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.SpinAttack);
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillCompleted -= SpinAttack;
        base.Exit();
        _triggerCalled = true;
    }
}
