using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldUltimate : Skill
{
    public UnityEvent OnSpawnPrisonEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        SoundManager.Play3DSound(SoundName.UltimateLance, transform.position);
        OnSpawnPrisonEvent?.Invoke();
    }
}
