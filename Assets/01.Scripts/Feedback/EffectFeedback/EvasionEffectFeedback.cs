using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionEffectFeedback : EffectFeedback
{
    [SerializeField] private float _hudTextEndTime;

    public override void StartFeedback()
    {
        HudTextPlayer effect = PoolManager.Instance.Pop($"EvasionHudText") as HudTextPlayer;
        effect.transform.position = transform.parent.position;
        effect.StartPlay(_hudTextEndTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
