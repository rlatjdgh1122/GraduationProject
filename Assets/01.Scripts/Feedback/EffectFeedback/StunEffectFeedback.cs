using UnityEngine;

public class StunEffectFeedback : EffectFeedback
{
    [SerializeField] private EffectPlayer _stunEffect;
    [SerializeField] private float _effectEndTime;

    public override void StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_stunEffect.name) as EffectPlayer;
        effect.transform.position = new Vector3(actionDataTrm.transform.position.x, actionDataTrm.transform.position.y + 1.5f, actionDataTrm.transform.position.z);
        effect.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {

    }
}
