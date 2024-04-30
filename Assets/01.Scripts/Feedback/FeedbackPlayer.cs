using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{

    public float Value { get; set; } = 0f;

    private List<Feedback> _feedbackToPlay = null;
    public List<Feedback> FeedbackToPlay => _feedbackToPlay;
    private void Awake()
    {
        SetUpEffect();
    }

    public void SetUpEffect()
    {
        _feedbackToPlay = new List<Feedback>();
        GetComponents<Feedback>(_feedbackToPlay);
    }

    public bool PlayFeedback()
    {
        bool result = true;

        //하나라도 False면 false를 리턴
        FinishFeedback();
        foreach (Feedback f in _feedbackToPlay)
        {
            f.Value = Value;
            result = f.StartFeedback();
        }

        return result;
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in _feedbackToPlay)
        {
            f.Value = 0f;
            f.FinishFeedback();
        }
    }
}
