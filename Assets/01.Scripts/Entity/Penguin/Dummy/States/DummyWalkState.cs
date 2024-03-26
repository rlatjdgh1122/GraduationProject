using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWalkState : DummyBaseState
{
    public DummyWalkState(Penguin penguin, EntityStateMachine<DummyPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _navAgent.speed = 1f;
        _penguin.MoveToPosition(GetRandomPoint());
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //�������� �����ϸ� Random�������
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
