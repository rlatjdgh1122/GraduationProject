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
    }

    public void CreateFeedback(Vector3 pos)
    {
        EffectPlayer effect = PoolManager.Instance.Pop("MortarGroundStrikeHit") as EffectPlayer;
        effect.transform.position = pos;
        effect.transform.rotation = transform.rotation;
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {

    }
}
