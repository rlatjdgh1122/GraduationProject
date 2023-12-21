using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectFeedback : Feedback
{
    [SerializeField] private PoolableMono _hitEffect;
    [SerializeField] private float _effectPlayTime;

    private EntityActionData _actionData;

    private void Awake()
    {
        _actionData = transform.Find("ActionData").GetComponent<EntityActionData>();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_hitEffect.name) as EffectPlayer;
        effect.transform.position = _actionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(_actionData.HitNormal * -1);
        effect.StartPlay(_effectPlayTime);
    }

    public override void FinishFeedback()
    {
   
    }
}
