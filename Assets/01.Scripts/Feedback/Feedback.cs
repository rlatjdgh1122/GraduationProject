using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Feedback : MonoBehaviour, IFeedback
{
    protected EntityActionData actionData;
    protected Transform actionDataTrm => actionData.transform;
    protected Entity owner;

    protected Animator _animator;
    protected CharacterController _controller;
    protected NavMeshAgent _navMeshAgent;
    protected Transform ownerTrm => owner.transform;

    public virtual float Value
    {
        //여기서 스탯가져와서 하면 강인함 가능
        get;
        set;
    }

    protected virtual void Start()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
        owner = transform.parent.GetComponent<Entity>();

        _animator = owner.AnimatorCompo;
        _controller = owner.CharacterCompo;
        _navMeshAgent = owner.NavAgent;
    }

    public abstract bool StartFeedback(); //피드백 생성
    public abstract bool FinishFeedback(); //피드백 종료

    protected virtual void OnDestroy()
    {
        FinishFeedback();
    }

    protected virtual void OnDisable()
    {
        FinishFeedback();
    }
}
