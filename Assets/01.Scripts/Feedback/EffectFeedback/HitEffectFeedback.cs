using UnityEngine;

public class HitEffectFeedback : EffectFeedback
{
    [SerializeField] private float _effectEndTime;

    protected override void Start()
    {
        base.Start();
    }

    public override void StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(actionData.HitType.ToString()) as EffectPlayer;  
        effect.transform.position = actionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(actionData.HitNormal * -1);
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {
   
    }
}
