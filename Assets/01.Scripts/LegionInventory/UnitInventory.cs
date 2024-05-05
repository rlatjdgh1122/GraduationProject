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

    public void LockSlot(PenguinTypeEnum type)
    {
        if (penguinDictionary == null) return;

        if(lockButtonDicntionary.TryGetValue(type, out UnitSlotUI slot))
        {
            slot.LockSlot();
        }
    }

    public void UnLockSlot(PenguinTypeEnum type)
    {
        if (penguinDictionary == null) return;

        if (lockButtonDicntionary.TryGetValue(type, out UnitSlotUI slot))
        {
            slot.UnLockSlot();
        }
    }
}