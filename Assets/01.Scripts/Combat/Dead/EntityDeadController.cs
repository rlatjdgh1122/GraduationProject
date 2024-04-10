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
    protected CharacterController _character;

    protected void Awake()
    {
        _owner = GetComponent<T>();
        _agent = GetComponent<NavMeshAgent>();
        _character = GetComponent<CharacterController>();
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
        //��ƼƼ �׺�޽��� �ݶ��̴� ����
        _agent.enabled = false;
        _character.enabled = false;
        //��ƼƼ ���� ó��
        _owner.IsDead = true;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = false;
    }
}
