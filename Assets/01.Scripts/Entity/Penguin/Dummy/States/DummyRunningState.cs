using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRunningState : DummyBaseState
{
    public DummyRunningState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //���ǵ�� �� ������
        _navAgent.speed = 3f;
        MoveToPosition(GetRandomPoint());
        _penguin.SetNavmeshPriority(2);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //�������� �����ϸ� Random�������
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
