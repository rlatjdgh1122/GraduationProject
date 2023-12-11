using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState<T> where T : Enum
{
    protected EnemyStateMachine<T> _stateMachine;

    protected Enemy _enemy;
    protected int _animBoolHash;

    protected Rigidbody2D _rigidbody;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����

    protected bool _triggerCalled;


    public EnemyState(Enemy enemyBase, EnemyStateMachine<T> stateMachine, string animBoolName)
    {
        _enemy = enemyBase;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _navAgent = _enemy.NavAgent;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
