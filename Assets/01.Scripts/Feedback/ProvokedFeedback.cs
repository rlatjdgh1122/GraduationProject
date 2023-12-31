using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokedFeedback : Feedback
{
    [SerializeField] private EffectPlayer _provokedEffect;
    [SerializeField] private float _effectEndTime;

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_provokedEffect.name) as EffectPlayer;
        effect.transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + 1.5f, owner.transform.position.z);
        effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
