using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDumbToDoState : DummyBaseState
{
    public readonly int AnimID = Animator.StringToHash("RandomValue");

    public DummyDumbToDoState(Penguin penguin, EntityStateMachine<DummyPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _penguin.StopImmediately();

        _penguin.AnimatorCompo.SetFloat(AnimID, _penguin.RandomValue);
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
