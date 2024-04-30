using UnityEngine;

public class ProvokedEffectFeedback : EffectFeedback
{
    public override bool StartFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop($"ProvokeEffect") as EffectPlayer;

        if (effect == null)
            return false;

        effect.transform.position = new Vector3(actionDataTrm.transform.position.x, actionDataTrm.transform.position.y + 1.5f, actionDataTrm.transform.position.z);
        effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        effect.StartPlay(Value);
        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
