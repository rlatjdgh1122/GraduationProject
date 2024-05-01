using UnityEngine;

public class DashEffectFeedback : EffectFeedback
{
    protected override void LoadEffect(string name)
    {
        base.LoadEffect("DashEffect");
    }

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
        EffectPlayer effect = PoolManager.Instance.Pop(_effect.ToString()) as EffectPlayer;
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
