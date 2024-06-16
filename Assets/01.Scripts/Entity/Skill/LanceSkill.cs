using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanceSkill : Skill
{
    public UnityEvent OnPrickEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Prick();   
    }

    public void Prick()
    {
        OnPrickEvent?.Invoke();
    }
}
