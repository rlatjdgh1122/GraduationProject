using System;

[Serializable]
public class LegionInventoryData
{
    public PenguinUIDataSO penguinData;
    public int stackSize;

    public LegionInventoryData(PenguinUIDataSO penguinData)
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
