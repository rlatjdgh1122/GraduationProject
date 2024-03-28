using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyState
{
    protected DummyStateMachine _stateMachine;

    protected DummyPenguin _penguin;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����

    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public DummyState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName)
    {
        _penguin = penguin;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
    }

    public virtual void Enter()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _navAgent = _penguin.NavAgent;
    }
    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
