using Define.Resources;
using UnityEngine;

public abstract class EffectFeedback : Feedback
{
    protected readonly string Path = "Effects";
    protected EffectPlayer _effect;

    [SerializeField] protected float _effectEndTime = 0.5f;

    protected virtual void LoadEffect(string name)
    {
        _effect = VResources.Load<EffectPlayer>($"{Path}/{name}");
    }
}
