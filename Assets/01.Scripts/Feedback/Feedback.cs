using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    protected EntityActionData actionData;
    protected Transform actionDataTrm => actionData.transform;
    protected Entity owner;
    protected Transform ownerTrm => owner.transform;

    protected virtual void Start()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
        owner = transform.parent.GetComponent<Entity>();
    }

    public abstract void CreateFeedback(); //피드백 생성
    public abstract void FinishFeedback(); //피드백 종료

    protected virtual void OnDestroy()
    {
        FinishFeedback();
    }

    protected virtual void OnDisable()
    {
        FinishFeedback();
    }
}
