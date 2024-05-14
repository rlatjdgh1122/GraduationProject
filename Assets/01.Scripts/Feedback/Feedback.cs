using StatOperator;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Feedback : MonoBehaviour, IFeedback
{
    protected EntityActionData actionData;
    protected Transform actionDataTrm => actionData.transform;
    protected Entity owner;
    protected BaseStat stat;

    protected Animator _animator;
    protected CharacterController _controller;
    protected NavMeshAgent _navMeshAgent;
    protected Transform ownerTrm => owner.transform;

    protected float _value = 0f;

    public virtual float Value
    {
        // ���⼭ ���� �����ͼ� �ϸ� ������ ����
        get
        {
            var percent = stat.tenacity.GetValue() / 100f;
            return _value * (1 - percent);
        }
        set { _value = value; }
    }

    public virtual void Awake()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
        owner = transform.parent.GetComponent<Entity>();
    }

    protected virtual void Start()
    {
        if (owner != null)
        {
            stat = owner.Stat;

            _animator = owner.AnimatorCompo;
            _controller = owner.CharacterCompo;
            _navMeshAgent = owner.NavAgent;
        }

    }

    public abstract bool StartFeedback(); //�ǵ�� ����
    public abstract bool FinishFeedback(); //�ǵ�� ����

    protected virtual void OnDestroy()
    {
        FinishFeedback();
    }

    protected virtual void OnDisable()
    {
        FinishFeedback();
    }
}
