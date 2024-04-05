using UnityEngine;
using UnityEngine.AI;

// �״°� Dead���·� ó���ϸ� ȣ�� ���� ������
// DeadTarget���� Ÿ���� ã�� �� ���� ��ƼƼ�� ã�� ������ ����
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
        // ��� bool ������ ��ȸ�ϸ� ��Ȱ��ȭ�ϱ�
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        //�״� �ִϸ��̼� ó��
        _anim.SetBool(HASH_DEAD, true);
        //��ƼƼ �׺�޽� ����
        _agent.enabled = false;
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = false;
    }
}
