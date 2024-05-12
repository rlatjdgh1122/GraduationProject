using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// �״°� Dead���·� ó���ϸ� ȣ�� ���� ������
// DeadTarget���� Ÿ���� ã�� �� ���� ��ƼƼ�� ã�� ������ ����
public abstract class EntityDeadController<T> : MonoBehaviour, IDeadable
    where T : Entity
{
    protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected Collider _collider;

    protected void Awake()
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
        _agent.enabled = true;
        _collider.enabled = true;
        //��ƼƼ �����
        _owner.IsDead = false;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = true;
    }
}
