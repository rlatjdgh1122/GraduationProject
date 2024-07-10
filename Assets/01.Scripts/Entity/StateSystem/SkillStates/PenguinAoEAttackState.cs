using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAoEAttackState : State
{
    public PenguinAoEAttackState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled) //АјАн
        {
            _stateMachine.ChangeState(PenguinStateType.Attack);
        }

        CheckCommandModeForMovement();
        CheckBattleModeForChase();
    }
    public override void ExitState()
    {
        base.ExitState();

        AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

    }
}
