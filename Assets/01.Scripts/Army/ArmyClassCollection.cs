using System.Collections.Generic;
using Unity.Burst.Intrinsics;
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

    public ArmyFollowCam FollowCam = null; //군단 오브젝트
    public ArmyInfo Info; //정보

    public List<Penguin> Soldiers = new(); //군인 펭귄들
    public General General = null; //장군

    public void AddStat(int value, StatType type, StatMode mode)
    {
        this.General?.AddStat(value, type, mode);
        foreach (var solider in this.Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }

    public void AddStat(List<Ability> abilities)
    {
        foreach (var incStat in abilities)
        {
            AddStat(incStat.value, incStat.statType, incStat.statMode);
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        this.General?.RemoveStat(value, type, mode);

        foreach (var solider in this.Soldiers)
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


