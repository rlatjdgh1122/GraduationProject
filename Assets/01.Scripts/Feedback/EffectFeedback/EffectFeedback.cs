using Define.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectFeedback : Feedback
{
    protected readonly string Path = "Effects";
    protected EffectPlayer _effect;

    protected virtual void LoadEffect(string name)
    {
        _effect = VResources.Load<EffectPlayer>($"{Path}/{name}");
    }
}
