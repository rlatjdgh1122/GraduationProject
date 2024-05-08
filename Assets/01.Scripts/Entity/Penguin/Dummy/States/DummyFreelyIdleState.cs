using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyFreelyIdleState : DummyBaseState
{
    private float timer = 0f;
    private float randomTime = 5f;

    public DummyFreelyIdleState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        timer = 0f;
        randomTime = Random.Range(1.0f, 5.0f);

        StopImmediately();

        ChangedAgentQuality(ObstacleAvoidanceType.NoObstacleAvoidance);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //���� �ð� ������ �������� ���º�ȯ
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            _stateMachine.ChangeState(RandomState());
        }


    }
    public override void Exit()
    {
        base.Exit();
    }


}
