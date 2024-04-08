using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : InitialUnitInventory
{
    public void PenguinSlotEnter(EntityInfoDataSO so)
    {
        if (penguinDictionary == null) return;

        if (penguinDictionary.TryGetValue(so, out UnitSlotUI slot))
        {
            slot.EnterSlot(so);
        }
    }

    public void PenguinSlotExit(EntityInfoDataSO so)
    {
        if (penguinDictionary == null) return;

        if (penguinDictionary.TryGetValue(so,out UnitSlotUI slot))
        {
            slot.ExitSlot(so);
        }
    }
}