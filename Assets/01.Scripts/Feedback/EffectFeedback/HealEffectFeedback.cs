using Define.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectFeedback : EffectFeedback  
{
    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop($"HealEffect") as EffectPlayer;

        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

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
