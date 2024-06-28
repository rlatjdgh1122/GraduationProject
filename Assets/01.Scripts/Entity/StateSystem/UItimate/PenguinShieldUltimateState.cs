using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinShieldUltimateState : State
{
    private General General => _penguin as General;

    public PenguinShieldUltimateState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        prevMode = _penguin.MyArmy.MovefocusMode;
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        General.Ultimate.PlaySkill();
    }

    public override void ExitState()
    {
        _penguin.MyArmy.MovefocusMode = prevMode;

        base.ExitState();
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
}
