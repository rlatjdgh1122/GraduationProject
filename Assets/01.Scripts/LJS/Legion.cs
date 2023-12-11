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

public class Legion : Singleton<Legion>
{
    public List<SlotUI> LegionUIList = new();

    public override void Init()
    {

    }
}
