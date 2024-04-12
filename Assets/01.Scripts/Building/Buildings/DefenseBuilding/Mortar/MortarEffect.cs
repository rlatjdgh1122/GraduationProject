using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEffect : Feedback
{
    [SerializeField] private float _effectEndTime;

    protected override void Start()
    {
        base.Start();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("MortarGroundStrikeHit") as EffectPlayer;
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {

    }
}
