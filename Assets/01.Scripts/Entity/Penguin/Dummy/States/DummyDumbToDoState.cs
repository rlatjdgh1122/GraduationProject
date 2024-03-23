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

        //���⼭�� �����۵� ������
        _penguin.AnimatorCompo.SetFloat(AnimID, _penguin.RandomValue);

        //IntŸ���� ���⼭�� �����۵� �����ʴ´�.
        _penguin.AnimatorCompo.SetInteger(AnimID, _penguin.RandomValue);

        _triggerCalled = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled) //�ִϸ��̼��� �����ٸ� Idle��
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
