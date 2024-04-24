using UnityEngine;

public class DashEffectFeedback : EffectFeedback
{
    [SerializeField] private float _effectEndTime = 0.5f;

    protected override void Start()
    {
        base.Start();
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("DashEffect") as EffectPlayer;
        if (effect != null)
        {
            effect.transform.position = gameObject.transform.position;
            effect.transform.rotation = gameObject.transform.rotation;
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
