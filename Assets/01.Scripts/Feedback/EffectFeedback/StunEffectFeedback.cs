using Define.Resources;
using UnityEngine;

public class StunEffectFeedback : EffectFeedback
{
    public override void Awake()
    {
        base.Awake();
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop($"StunEffect") as EffectPlayer;
        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

            effect.transform.position = new Vector3(actionDataTrm.transform.position.x, actionDataTrm.transform.position.y + 1.5f, actionDataTrm.transform.position.z);
            effect.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            effect.StartPlay(Value);

            return true;
        }
        return false;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
