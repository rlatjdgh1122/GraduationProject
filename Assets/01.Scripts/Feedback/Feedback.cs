using UnityEngine;

public abstract class Feedback : MonoBehaviour, IFeedback
{
    protected EntityActionData actionData;
    protected Transform actionDataTrm => actionData.transform;
    protected Entity owner;
    protected Transform ownerTrm => owner.transform;

    public virtual float Value { get; set; }

    protected virtual void Start()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
        owner = transform.parent.GetComponent<Entity>();
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
