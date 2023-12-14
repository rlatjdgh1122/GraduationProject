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
    public int Sword;
    public int Arrow;
    public Transform _LegionPannel;
}

public class Legion : Singleton<Legion>
{
    public List<SlotUI> LegionUIList = new();
    public List<LegionCnt> LegionCnt = new();
}
