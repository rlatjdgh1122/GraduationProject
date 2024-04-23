using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectFeedback : EffectFeedback  
{
    [SerializeField] private EffectPlayer _healEffect;
    [SerializeField] private float _effectEndTime;

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_healEffect.name) as EffectPlayer;

        if (effect != null)
        {
            effect.transform.position = actionDataTrm.transform.position;
            effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
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
