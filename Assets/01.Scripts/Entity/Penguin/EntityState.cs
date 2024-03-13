using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityState<T, G> where T : Enum where G : Entity
{
    protected EntityStateMachine<T, G> _stateMachine;
    protected G _penguin;
    protected NavMeshAgent _navAgent; //편의를 위해서 여기에도 NavAgent 선언

    protected int _animBoolHash;
    protected bool _triggerCalled = true;


    public EntityState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName)
    {
        _penguin = penguin;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
    }

    public virtual void Enter()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _penguin.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;

        if (_penguin is Penguin)
        {
            var penguin = _penguin as Penguin;
            penguin.WaitForCommandToArmyCalled = true;
        }
    }
}
