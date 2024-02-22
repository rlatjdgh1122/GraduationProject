using System;

[Serializable]
public class LegionInventoryData
{
    public PenguinStat penguinData;
    public int stackSize;

    public LegionInventoryData(PenguinStat penguinData)
    {
        this.penguinData = penguinData;
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
