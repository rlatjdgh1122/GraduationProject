using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinIdleState : State
{
    public PenguinIdleState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(PenguinStateType.Move);

        if (_penguin.MoveFocusMode == ArmySystem.MovefocusMode.Battle && _penguin.MyArmy.IsArmyReady)
        {
            if (_penguin.IsTargetInInnerRange)
                _stateMachine.ChangeState(PenguinStateType.Chase);
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();
    }
}
