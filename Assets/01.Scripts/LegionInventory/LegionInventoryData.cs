using System;

[Serializable]
public class LegionInventoryData
{
    public EntityInfoDataSO InfoData;

    public string LegionName
    {
        get => InfoData.LegionName;
        set { InfoData.LegionName = value; }
    }

    public int IndexNumber
    {
        get => InfoData.SlotIdx;
        set { InfoData.SlotIdx = value; }
    }

    public float CurrentHPPercent;

    /// <param name="InfoData"></param>
    /// <param name="LegionName">�� ��������</param>
    public LegionInventoryData(EntityInfoDataSO InfoData, string LegionName, int Idx)
    {
        this.InfoData = InfoData;
        this.LegionName = LegionName;
        this.IndexNumber = Idx;
    }

    public void HPPercent(float percent)
    {
        CurrentHPPercent = percent;
    }
}
