using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackToPlay = null;

    private Entity _owner;

    private void Awake()
    {
        _owner = transform.parent.GetComponent<Entity>();
        _feedbackToPlay = new List<Feedback>();
        GetComponents<Feedback>(_feedbackToPlay); 

        foreach (Feedback feedback in _feedbackToPlay)
        {
            feedback.owner = _owner;
        }
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (Feedback f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in _feedbackToPlay)
        {
            f.FinishFeedback();
        }
    }
}
