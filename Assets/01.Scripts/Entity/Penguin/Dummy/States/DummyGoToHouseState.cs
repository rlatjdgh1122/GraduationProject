using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGoToHouseState : DummyBaseState
{
    public DummyGoToHouseState(DummyPenguin penguin, EntityStateMachine<DummyPenguinStateEnum, DummyPenguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _navAgent.SetDestination(_penguin.HouseTrm.position);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if(_navAgent.remainingDistance < 0.05f)
        {
            _penguin.GoToHouse();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

   
}
