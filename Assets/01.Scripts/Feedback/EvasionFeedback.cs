using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionFeedback : Feedback
{
    [SerializeField] private float _hudTextEndTime;

    public override void CreateFeedback()
    {
        HudTextPlayer effect = PoolManager.Instance.Pop($"EvasionHudText") as HudTextPlayer;
        effect.transform.position = transform.parent.position;
        effect.StartPlay(_hudTextEndTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
