using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState<T> where T : Enum
{
    protected EnemyStateMachine<T> _stateMachine;

    protected Enemy _enemy;
    protected int _animBoolHash;

    protected Rigidbody2D _rigidbody;
    protected NavMeshAgent _navAgent; //편의를 위해서 여기에도 NavAgent 선언

    protected bool _triggerCalled;


    public EnemyState(Enemy enemyBase, EnemyStateMachine<T> stateMachine, string animBoolName)
    {
        _enemy = enemyBase;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _enemy.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
