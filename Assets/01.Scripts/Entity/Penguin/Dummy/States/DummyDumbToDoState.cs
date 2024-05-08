using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyDumbToDoState : DummyBaseState
{
    public readonly int AnimID = Animator.StringToHash("RandomValue");

    public DummyDumbToDoState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StopImmediately();

        _penguin.AnimatorCompo.SetFloat(AnimID, _penguin.RandomValue);

        ChangedAgentQuality(ObstacleAvoidanceType.NoObstacleAvoidance);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled) //�ִϸ��̼��� �����ٸ� Idle��
        {
            _stateMachine.ChangeState(DummyPenguinStateEnum.FreelyIdle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _triggerCalled = false;
    }

}
