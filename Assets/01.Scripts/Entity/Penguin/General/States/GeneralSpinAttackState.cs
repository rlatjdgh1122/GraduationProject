using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpinAttackState : GeneralBaseState
{
    public GeneralSpinAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            if (!_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
