using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildEffectFeedback : Feedback
{
    [SerializeField] private float _effectEndTime;

    protected override void Start()
    {
        base.Start();
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(actionData.HitType.ToString()) as EffectPlayer;

        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.StartPlay(_effectEndTime);

            return true;
        }

        return false;

    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
