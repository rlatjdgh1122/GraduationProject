using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.GridLayoutGroup;

public class SingijeonUltimate : Skill
{
    public UnityEvent OnUltimateEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        OnUltimateEvent?.Invoke();
    }
}
