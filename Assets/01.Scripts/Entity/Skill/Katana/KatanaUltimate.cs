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

        _changeVolume = FindAnyObjectByType<ChangeVolume>(); // �����ⰰ�� ����̴ϱ� ���߿� �ٲ�ߵ�
    }

    public override void PlaySkill()
    {
        OnStartEclipseEvent?.Invoke();
        _changeVolume.ChangeVolumeEffect(VolumeType.LunarEclipse, 3f);
        AddStat();
        CoroutineUtil.CallWaitForSeconds(4, () => RemoveStat()); //LunarEclipseUI���� 4�淡 4�� ��
    }

    private void AddStat()
    {
        _myArmy.Soldiers.ForEach(s => s.Stat.AddStat(buffValue, _buffStatType, StatMode.Increase));
    }

    private void RemoveStat()
    {
        _myArmy.Soldiers.ForEach(s => s.Stat.RemoveStat(buffValue, _buffStatType, StatMode.Increase));
    }
}
