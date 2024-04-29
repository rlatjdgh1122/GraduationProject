using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerPenguinIdleState : SignalTowerPenguinBaseState
{
    public SignalTowerPenguinIdleState(Penguin penguin, EntityStateMachine<SignalTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _triggerCalled = true;
        SignalHub.OnIceArrivedEvent += FindTarget;
    }

    private void FindTarget()
    {
        _penguin.FindNearestTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(SignalTowerPenguinStateEnum.Watch);
    }

    public override void Exit()
    {
        SignalHub.OnIceArrivedEvent -= FindTarget;
        base.Exit();
    }
}
