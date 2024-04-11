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
    public string LegionName; //몇번째 군단
    public bool IsCanReadyAttackInCurArmySoldiersList = true; //군단 전체가 움직일 준비가 되었는가
    public List<Penguin> Soldiers = new(); //군인 펭귄들
    public General General = null; //장군

    public ArmyFollowCam FollowCam = null; //군단 오브젝트
    public ArmyInfo Info; //정보

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
    public void RemoveStat(Army army, int value, StatType type, StatMode mode)
    {
        army.General?.RemoveStat(value, type, mode);

        foreach (var solider in army.Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
}
public class ArmyFollowCam
{
    public GameObject Obj = null;
    public Vector3 mousePos = Vector3.zero;
    public float moveSpeed = 5f;
    public bool isInGeneral = false;
}


