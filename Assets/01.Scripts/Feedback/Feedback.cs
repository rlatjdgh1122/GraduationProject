using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public Entity owner;

    protected virtual void Start()
    {

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
