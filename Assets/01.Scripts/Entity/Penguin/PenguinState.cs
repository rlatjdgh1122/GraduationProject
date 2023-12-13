using System;
using UnityEngine;
using UnityEngine.AI;

public class PenguinState<T> where T : Enum
{
    protected PenguinStateMachine<T> _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����

    protected int _animBoolHash;
    protected bool _triggerCalled;
    

    public PenguinState(Penguin penguin, PenguinStateMachine<T> stateMachine, string animationBoolName)
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

    public void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
