using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionEffectFeedback : EffectFeedback
{
    [SerializeField] private float _hudTextEndTime = 0.5f;

    protected override void LoadEffect(string name)
    {
        base.LoadEffect("HealEffect");
    }
    public override bool StartFeedback()
    {
        HudTextPlayer effect = PoolManager.Instance.Pop($"EvasionHudText") as HudTextPlayer;

        if( effect != null)
        {
            effect.transform.position = transform.parent.position;
            effect.StartPlay(_hudTextEndTime);

            return true;
        }

        return false;
       
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
