using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KatanaUltimate : Skill
{
    public UnityEvent OnStartEclipseEvent;

    private ChangeVolume _changeVolume;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);

        _changeVolume = FindAnyObjectByType<ChangeVolume>();
    }

    public override void PlaySkill()
    {
        OnStartEclipseEvent?.Invoke();
        _changeVolume.ChangeVolumeEffect(VolumeType.LunarEclipse, 3f);
    }
}
