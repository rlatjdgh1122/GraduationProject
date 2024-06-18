using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePrickEffectFeedback : EffectFeedback
{
    public override bool FinishFeedback()
    {
        return true;
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("LancePrickEffect") as EffectPlayer;
        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

            effect.transform.position = gameObject.transform.position;
            effect.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, -90, 0);
            effect.StartPlay(_effectEndTime);

            return true;
        }

        return false;
    }
}
