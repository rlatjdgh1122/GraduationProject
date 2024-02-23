using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerState<T> where T : Enum
{
    protected WorkerStateMachine<T> _stateMachine;
    protected Worker _worker;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����

    protected int _animBoolHash;
    protected bool _triggerCalled = true;


    public WorkerState(Worker worker, WorkerStateMachine<T> stateMachine, string animationBoolName)
    {
        _worker = worker;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
    }

    public virtual void Enter()
    {
        _worker.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _navAgent = _worker.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _worker.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
