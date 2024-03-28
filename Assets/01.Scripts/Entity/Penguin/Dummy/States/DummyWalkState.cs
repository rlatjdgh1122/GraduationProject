using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWalkState : DummyBaseState
{
    public DummyWalkState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _navAgent.speed = 1f;
        MoveToPosition(GetRandomPoint());
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //목적지에 도달하면 Random모션으로
        if (_navAgent.remainingDistance < 0.05f)
        {
            _stateMachine.ChangeState(RandomState());
        }
    }
    public override void Exit()
    {
        base.Exit();
    }


}
