using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectFeedback : Feedback
{
    [SerializeField] private float _effectEndTime;

    private EntityActionData _actionData;

    protected override void Start()
    {
        _actionData = owner.ActionData;
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_actionData.HitType.ToString()) as EffectPlayer;  
        effect.transform.position = _actionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(_actionData.HitNormal * -1);
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {
   
    }
}
