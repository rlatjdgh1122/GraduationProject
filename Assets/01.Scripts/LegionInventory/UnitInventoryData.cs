using System;
public enum SlotType
{
    Unit,
    Legion
}

[Serializable]
public class UnitInventoryData
{
    public EntityInfoDataSO InfoData;
    public int StackSize;
    public SlotType SlotType;

    public UnitInventoryData(EntityInfoDataSO infoData, int stackSize, SlotType slottype = SlotType.Unit)
    {
        this.InfoData = infoData;
        this.StackSize = stackSize;
        this.SlotType = slottype;
    }

    public void RemoveStack(int count = 1)
    {
        StackSize -= count;

        if(StackSize <= 0)
            LegionInventoryManager.Instance.SelectInfoData(null);
    }
}