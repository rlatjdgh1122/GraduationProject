using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectFeedback : Feedback
{
    [SerializeField] private float _effectPlayTime;

    private EntityActionData _actionData;

    private void Awake()
    {
        _actionData = transform.parent.GetComponent<EntityActionData>();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_actionData.HitType.ToString()) as EffectPlayer;  
        effect.transform.position = _actionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(_actionData.HitNormal * -1);
        effect.StartPlay(_effectPlayTime);
    }

    public override void FinishFeedback()
    {
   
    }
}
