using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ArmyInfo //UI부분, 기획이 더 필요
{
    //인원수
    public int totalCount;
    public int basicCount;
    public int archerCount;
    public int shieldCount;

    //스탯이 몇배정도 증가하였는가
    public float basicTimes;
    public float archerTimes;
    public float shieldTimes;
}

[System.Serializable]
public class Army
{
    public int Legion; //몇번째 군단
    public bool IsMoving; //움직이는 중인가
    public List<Penguin> Soldiers = new(); //군인 펭귄들
    public General General; //장군

    public ArmyInfo info;

    public void AddStat(Army army, LigeonStatAdjustment ligeonStat)
    {
        var IncStatList = ligeonStat.IncStat;
        var DecStatList = ligeonStat.DecStat;

        foreach (var incStat in IncStatList)
        {
            AddStat(army, incStat.value, incStat.type, StatMode.Increase);
        }

        foreach (var DecStat in DecStatList)
        {
            AddStat(army, DecStat.value, DecStat.type, StatMode.Decrease);
        }
    }
    public void AddStat(Army army, int value, StatType type, StatMode mode)
    {
        army.General?.AddStat(value, type, mode);
        foreach (var solider in army.Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }
    public void RemoveStat(Army army,int value, StatType type, StatMode mode)
    {
        army.General?.RemoveStat(value, type, mode);

        foreach (var solider in army.Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
}


