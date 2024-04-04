using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DeadEntity<T> : MonoBehaviour, IDeadable where T : Entity
{
    private readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    private Animator _anim;
    private NavMeshAgent _agent;

    protected void Awake()
    {
        _owner = GetComponent<T>();
        _agent = GetComponent<NavMeshAgent>();

        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public virtual void OnDied()
    {
        //�״� �ִϸ��̼�
        _anim.SetBool(HASH_DEAD, true);
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ �׺�޽� ����
        _agent.enabled = false;
    }
}
