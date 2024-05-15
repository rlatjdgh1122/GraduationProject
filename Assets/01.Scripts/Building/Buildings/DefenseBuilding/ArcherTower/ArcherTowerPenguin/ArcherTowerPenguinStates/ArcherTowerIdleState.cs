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
        SignalHub.OnGroundArrivedEvent += FindTarget;
    }

    private void FindTarget()
    {
        _penguin.FindNearestEnemy();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsTargetInAttackRange)
            _stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        SignalHub.OnGroundArrivedEvent -= FindTarget;
        base.Exit();
    }
}
