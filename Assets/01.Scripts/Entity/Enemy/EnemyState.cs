using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    protected EnemyStateMachine _stateMachine;

    protected Enemy _enemy;
    protected NavMeshAgent _navAgent;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
        _navAgent = _enemy.NavAgent;
    }

    #region Base
    public virtual void EnterState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _enemy.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
    #endregion



   
}
