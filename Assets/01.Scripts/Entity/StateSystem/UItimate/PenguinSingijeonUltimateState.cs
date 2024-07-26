using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSingijeonUltimateState : State
{
    private General General => _penguin as General;

    public PenguinSingijeonUltimateState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        General.Skill.PlaySkill();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Attack);
            }
            else
            {
                _stateMachine.ChangeState(PenguinStateType.Idle);
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
