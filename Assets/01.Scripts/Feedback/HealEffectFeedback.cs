using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectFeedback : Feedback
{
    [SerializeField] private EffectPlayer _healEffect;
    [SerializeField] private float _effectEndTime;

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_healEffect.name) as EffectPlayer;
        effect.transform.SetParent(owner.transform);
        effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {
           
    }
}
