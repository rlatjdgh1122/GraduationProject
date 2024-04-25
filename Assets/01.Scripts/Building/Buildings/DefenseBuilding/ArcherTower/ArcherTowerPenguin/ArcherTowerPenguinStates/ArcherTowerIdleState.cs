using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerIdleState : ArcherTowerBaseState
{
    public ArcherTowerIdleState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
        _penguin.SetTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        SignalHub.OnIceArrivedEvent -= FindTarget;
        base.Exit();
    }
}
