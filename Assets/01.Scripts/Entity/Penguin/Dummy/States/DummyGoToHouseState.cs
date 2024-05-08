using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyGoToHouseState : DummyBaseState
{
    public DummyGoToHouseState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _navAgent.speed = 5f;
        _navAgent.isStopped = false;

        _navAgent.SetDestination(_penguin.HouseTrm.position);

        ChangedAgentQuality(ObstacleAvoidanceType.NoObstacleAvoidance, 0);

    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_navAgent.remainingDistance < 0.05f)
        {
            _penguin.GoToHouse();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


}
