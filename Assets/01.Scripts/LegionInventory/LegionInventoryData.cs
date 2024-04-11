using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class LegionInventoryData
{
    public EntityInfoDataSO InfoData;
    public string LegionName
    {
        get => InfoData.LegionName;
        set {
            InfoData.LegionName = value;
        }
    }

    public int SlotIdx
    {
        get => InfoData.SlotIdx;
        set { InfoData.SlotIdx = value; }
    }

    public float CurrentHPPercent;

    /// <param name="InfoData"></param>
    /// <param name="LegionName">몇 군단인지</param>
    public LegionInventoryData(EntityInfoDataSO InfoData, string LegionName, int Idx)
    {
        this.InfoData = InfoData;
        this.LegionName = LegionName;
        this.SlotIdx = Idx;
    }

    public void HPPercent(float percent)
    {
        CurrentHPPercent = percent;
    }
}
