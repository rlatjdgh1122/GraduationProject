using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectFeedback : Feedback
{
    [SerializeField] private EffectPlayer _effect;
    public EffectPlayer Effect => _effect;

    private EffectPlayer _curEffect;

    private float effectTime = Mathf.Infinity;

    public void SetUpEffect(BuffEffectFeedback buffEffect, float effectTime)
    {
        _effect = buffEffect.Effect;
        this.effectTime = effectTime;
    }

    public override bool StartFeedback()
    {
        if (_curEffect != null)
        {
            return false;
        }

        _curEffect = PoolManager.Instance.Pop(_effect.name) as EffectPlayer;
        _curEffect.transform.position = transform.position;
        _curEffect.transform.SetParent(null);

        var main = _curEffect.Particles[0].main;
        main.startSize = 1.5f;

        _curEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        _curEffect.StartPlay(effectTime);

        return true;
    }

    public override bool FinishFeedback()
    {
        if (_curEffect == null)
            return false;

        PoolManager.Instance.Push(_curEffect);
        Debug.Log(_curEffect);
        _curEffect = null;

        return true;
    }
}
