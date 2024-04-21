using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFeedbackPlayer : MonoBehaviour
{
    public void SpawnFeedback<T>() where T : Feedback
    {
        GameObject obj = new GameObject(typeof(T).Name);
        obj.AddComponent<FeedbackPlayer>();
        obj.AddComponent<T>();

        obj.transform.parent = transform;
    }
}
