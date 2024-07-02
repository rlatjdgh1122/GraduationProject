using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KatanaUltimate : Skill
{
    [Range(0, 100)]
    [SerializeField]
    private int buffValue;

    [SerializeField]
    private StatType _buffStatType;

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
        AddStat();
        CoroutineUtil.CallWaitForSeconds(4, () => RemoveStat()); // LunarEclipseUI에서 4길래 4로 함
    }

    private void AddStat()
    {
        _myArmy.General.AddStat(buffValue, _buffStatType, StatMode.Increase);
        _myArmy.General.StrengthBuffEffect?.ParticleStart();

        foreach (Penguin penguin in _myArmy.Soldiers)
        {
            penguin.Stat.AddStat(buffValue, _buffStatType, StatMode.Increase);
            Debug.Log(penguin.StrengthBuffEffect);
            penguin.StrengthBuffEffect?.ParticleStart();
        }
    }

    private void RemoveStat()
    {
        _myArmy.General.RemoveStat(buffValue, _buffStatType, StatMode.Increase);
        _myArmy.General.StrengthBuffEffect?.ParticleStop();

        foreach (Penguin penguin in _myArmy.Soldiers)
        {
            penguin.Stat.RemoveStat(buffValue, _buffStatType, StatMode.Increase);
            penguin.StrengthBuffEffect?.ParticleStop();
        }
    }
}
