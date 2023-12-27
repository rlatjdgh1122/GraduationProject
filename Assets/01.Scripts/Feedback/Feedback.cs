using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public Entity owner;

    protected virtual void Start()
    {

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
