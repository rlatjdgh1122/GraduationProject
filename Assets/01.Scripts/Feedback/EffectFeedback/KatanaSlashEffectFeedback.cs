using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaSlashEffectFeedback : EffectFeedback
{
    public override bool FinishFeedback()
    {
        return true;
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("KatanaSlashHit") as EffectPlayer;
        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

            effect.transform.position = gameObject.transform.position;
            effect.transform.rotation = gameObject.transform.rotation;
            effect.StartPlay(_effectEndTime);

            return true;
        }

        return false;
    }
}
