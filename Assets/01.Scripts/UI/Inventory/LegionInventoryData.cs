using System;

[Serializable]
public class LegionInventoryData
{
    public PenguinInfoDataSO infoData;
    public int stackSize;

    public LegionInventoryData(PenguinInfoDataSO InfoData)
    {
        this.infoData = InfoData;
        AddStack();
    }

    public void AddStack()
    {
        ++stackSize;
    }

    public void RemoveStack(int count = 1)
    {
        stackSize -= count;
    }
}
