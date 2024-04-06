using System;

[Serializable]
public class LegionInventoryData
{
    public EntityInfoDataSO InfoData;

    public string LegionName;

    public int IndexNumber;

    /// <param name="InfoData"></param>
    /// <param name="LegionName">�� ��������</param>
    public LegionInventoryData(EntityInfoDataSO InfoData, string LegionName, int Idx)
    {
        this.InfoData = InfoData;
        this.LegionName = LegionName;
        this.IndexNumber = Idx;
    }
}
