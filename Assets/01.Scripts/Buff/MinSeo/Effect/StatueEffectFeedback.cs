using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEffectFeedback : Feedback
{
    [SerializeField] private EffectPlayer _effect;

    private EffectPlayer _curEffect;

    public override void CreateFeedback()
    {
        if (_curEffect != null) { return; }
        _curEffect = PoolManager.Instance.Pop(_effect.name) as EffectPlayer;
        _curEffect.transform.position = transform.position;
        _curEffect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        _curEffect.StartPlay(Mathf.Infinity);
    }

    public override void FinishFeedback()
    {
        if(_curEffect != null)
        {
            PoolManager.Instance.Push(_curEffect);
            _curEffect = null;
        }
    }
}
