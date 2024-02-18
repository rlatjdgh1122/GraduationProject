using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerState<T> where T : Enum
{
    protected WorkerStateMachine<T> _stateMachine;
    protected Worker _worker;
    protected NavMeshAgent _navAgent; //편의를 위해서 여기에도 NavAgent 선언

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
        _worker.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _worker.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _worker.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
