using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEffectFeedback : Feedback
{
    [SerializeField] private EffectPlayer _effect;

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_effect.name) as EffectPlayer;
        effect.transform.position = transform.position;
        effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        effect.StartPlay(Mathf.Infinity);
    }

    public override void FinishFeedback()
    {
    }
}
