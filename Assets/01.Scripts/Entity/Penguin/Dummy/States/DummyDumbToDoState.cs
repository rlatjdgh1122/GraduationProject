using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDumbToDoState : DummyBaseState
{

    public readonly int AnimID = Animator.StringToHash("RandomValue");
    public DummyDumbToDoState(DummyPenguin penguin, EntityStateMachine<DummyPenguinStateEnum, DummyPenguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _penguin.StopImmediately();

        //여기서는 정상작동 하지만
        _penguin.AnimatorCompo.SetFloat(AnimID, _penguin.RandomValue);

        //Int타입인 여기서는 정상작동 하지않는다.
        _penguin.AnimatorCompo.SetInteger(AnimID, _penguin.RandomValue);

        _triggerCalled = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled) //애니메이션이 끝낫다면 Idle로
        {
            _stateMachine.ChangeState(DummyPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _triggerCalled = false;
    }

}
