using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceUltimateEffectFeedback : EffectFeedback
{
    public override bool FinishFeedback()
    {
        return true;
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("LanceUltimateEffect") as EffectPlayer;

        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

            effect.transform.position = gameObject.transform.position - gameObject.transform.forward * -5;
            effect.transform.rotation = gameObject.transform.rotation;
            effect.StartPlay(_effectEndTime);

            return true;
        }

        return false;
    }
}
