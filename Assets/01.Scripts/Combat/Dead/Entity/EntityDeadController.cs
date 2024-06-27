using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// �״°� Dead���·� ó���ϸ� ȣ�� ���� ������
// DeadTarget���� Ÿ���� ã�� �� ���� ��ƼƼ�� ã�� ������ ����
public abstract class EntityDeadController<T> : PoolableMono, IDeadable
    where T : Entity
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected Collider _collider;

    protected virtual void Awake()
    {
        _owner = GetComponent<T>();
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public virtual void OnDied()
    {

        // ��� bool ������ ��ȸ�ϸ� ��Ȱ��ȭ�ϱ�
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        _anim.speed = 1f;

        //�״� �ִϸ��̼� ó��
        _anim.SetBool(HASH_DEAD, true);
        //��ƼƼ �׺�޽��� �ݶ��̴� ����
        _agent.enabled = false;
        _collider.enabled = false;
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = false;

        SignalHub.OnBattlePhaseEndEvent += PushObj;
    }

    public virtual void OnResurrected()
    {
        //ü�� �ٽ� ä��
        _owner.HealthCompo.SetHealth(_owner.Stat);

        _anim.speed = 1f;
        //�ִϸ��̼� ����
        _anim.enabled = true;
        //�״� �ִϸ��̼� ����
        _anim.SetBool(HASH_DEAD, false);
        //��ƼƼ �׺�޽��� �ݶ��̴� ����
        _collider.enabled = true;
        _agent.enabled = true;
        //��ƼƼ �����
        _owner.IsDead = false;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = true;
    }

    protected virtual void PushObj()
    {
        PoolManager.Instance.Push(_owner);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= PushObj;
    }
}
