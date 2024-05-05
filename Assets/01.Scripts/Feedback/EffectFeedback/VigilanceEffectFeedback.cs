using UnityEngine;

public class VigilanceEffectFeedback : EffectFeedback
{

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop($"VigilanceEffect") as EffectPlayer;
        if (effect != null)
        {
            effect.transform.SetParent(ownerTrm);

            effect.transform.position = ownerTrm.position;
            effect.transform.rotation = ownerTrm.rotation;
            effect.transform.localScale = ownerTrm.lossyScale;
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
