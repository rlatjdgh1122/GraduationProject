using UnityEngine;

public class HitEffectFeedback : EffectFeedback
{
    [SerializeField] private float _effectEndTime = 0.5f;

    protected override void Start()
    {
        base.Start();
    }

    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(actionData.HitType.ToString()) as EffectPlayer;
        if (effect != null)
        {
            effect.transform.position = actionData.HitPoint;
            effect.transform.rotation = Quaternion.LookRotation(actionData.HitNormal * -1);
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
