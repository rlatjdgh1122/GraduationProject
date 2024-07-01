using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KatanaUltimate : Skill
{
    public UnityEvent OnStartEclipseEvent;

    private ChangeVolume _changeVolume;

    Army _myArmy = null;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);

        General general = owner as General;
        _myArmy = general.MyArmy;

        _changeVolume = FindAnyObjectByType<ChangeVolume>(); // 스레기같은 방법이니까 나중에 바꿔야됨
    }

    public override void PlaySkill()
    {
        OnStartEclipseEvent?.Invoke();
        _changeVolume.ChangeVolumeEffect(VolumeType.LunarEclipse, 3f);
    }
}
