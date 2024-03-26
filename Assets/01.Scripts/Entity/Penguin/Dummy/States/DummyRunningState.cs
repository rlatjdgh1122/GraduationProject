using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRunningState : DummyBaseState
{
    public DummyRunningState(Penguin penguin, EntityStateMachine<DummyPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //���ǵ�� �� ������
        _navAgent.speed = 3f;
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
