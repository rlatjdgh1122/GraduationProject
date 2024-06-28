using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanceUltimate : Skill
{
    public UnityEvent OnSpawnTruckEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        OnSpawnTruckEvent?.Invoke();
    }
}
