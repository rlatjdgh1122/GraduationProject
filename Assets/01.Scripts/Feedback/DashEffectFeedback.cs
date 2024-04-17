using UnityEngine;

public class DashEffectFeedback : Feedback
{
    [SerializeField] private float _effectEndTime;

    protected override void Start()
    {
        base.Start();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop("DashEffect") as EffectPlayer;
        effect.transform.position = gameObject.transform.position;
        effect.transform.rotation = gameObject.transform.rotation;
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {

    }
}
