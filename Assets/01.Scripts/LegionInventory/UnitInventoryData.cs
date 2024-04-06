using System;

[Serializable]
public class UnitInventoryData
{
    public EntityInfoDataSO infoData;
    public int stackSize;

    public UnitInventoryData(EntityInfoDataSO InfoData, int StackSize)
    {
        this.infoData = InfoData;
        this.stackSize = StackSize;
    }

    public void RemoveStack(int count = 1)
    {
        stackSize -= count;

        if(stackSize <= 0)
            LegionInventoryManager.Instance.SelectInfoData(null);
    }
}
