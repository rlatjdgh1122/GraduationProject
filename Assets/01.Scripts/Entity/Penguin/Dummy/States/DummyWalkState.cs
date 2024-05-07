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
        _penguin.SetNavmeshPriority(2);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //목적지에 도달하거나 앞에 뭐가 있다면 Random모션으로
        if (IsSomethingInFront() || _navAgent.remainingDistance < 0.05f)
        {
            _stateMachine.ChangeState(RandomState());
        }
    }
    public override void Exit()
    {
        base.Exit();
        _penguin.SetNavmeshPriority(1);
    }


}
