using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotUI
{
    public int HeroCnt;
    public GameObject SlotUIPrefab;
}

[Serializable]
public class LegionCnt
{
    public int SpawnSwordCnt;
    public int SpawnArrowCnt;
    public int SpawnShieldCnt;

    public int TotalSwordCnt = 0;
    public int TotalArrowCnt = 0;
    public int TotalShieldCnt = 0;
    public Transform _LegionPannel;
}

public class Legion : Singleton<Legion>
{
    public List<SlotUI> LegionUIList = new();
    public List<LegionCnt> LegionCnt = new();

}
