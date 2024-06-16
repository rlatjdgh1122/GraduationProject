using ArmySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBlockState : State
{
    public PenguinBlockState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.StopImmediately();
        _penguin.FindNearestEnemyInTargetArmy();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();
        //사거리가 멀어지면 맞으러 감
        if (!_penguin.IsTargetInAttackRange)
            _stateMachine.ChangeState(PenguinStateType.Chase);

        IsTargetNull(PenguinStateType.Idle);


        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }

        CheckBattleModeForChase();
        CheckCommandModeForMovement();

    }//end method

    public override void ExitState()
    {
        base.ExitState();
    }
}
