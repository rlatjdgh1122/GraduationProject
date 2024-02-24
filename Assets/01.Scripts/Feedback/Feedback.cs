using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    protected EntityActionData actionData;
    protected Transform ownerTrm => actionData.transform;

    protected virtual void Start()
    {
        actionData = transform.parent.GetComponent<EntityActionData>();
    }

    public abstract void CreateFeedback(); //�ǵ�� ����
    public abstract void FinishFeedback(); //�ǵ�� ����

    protected virtual void OnDestroy()
    {
        FinishFeedback();
    }

    protected virtual void OnDisable()
    {
        FinishFeedback();
    }
}
