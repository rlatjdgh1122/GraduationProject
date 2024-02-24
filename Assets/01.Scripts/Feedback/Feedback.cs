using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    protected EntityActionData actionData;
    protected Transform ownerTrm => actionData.transform;

    protected virtual void Start()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
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
