using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinThrowState : State
{
    private General General => _penguin as General;

    public PenguinThrowState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        if (_penguin.CurrentTarget == null)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }
        else
        {
            _triggerCalled = false;
            _penguin.CurrentTarget.transform.position = _penguin.transform.position;
            General.skill.PlaySkill();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
