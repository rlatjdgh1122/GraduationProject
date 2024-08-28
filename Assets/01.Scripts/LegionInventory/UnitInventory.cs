using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : InitialUnitInventory
{
    public void PenguinSlotEnter(EntityInfoDataSO so) //�κ��丮�� ����� �߰����� ��
    {
        if (penguinDictionary.TryGetValue(so, out UnitSlotUI slot))
        {
            slot.EnterSlot(so);
        }
    }

    public void PenguinSlotExit(EntityInfoDataSO so) //�κ��丮�� ����� ������ ��
    {
        if (penguinDictionary.TryGetValue(so,out UnitSlotUI slot))
        {
            slot.ExitSlot(so);
        }
    }

    public void LockSlot(PenguinTypeEnum type)
    {
        if(lockButtonDicntionary.TryGetValue(type, out UnitSlotUI slot))
        {
            slot.LockSlot();
        }
    }

    public void UnLockSlot(PenguinTypeEnum type)
    {
        if (lockButtonDicntionary.TryGetValue(type, out UnitSlotUI slot))
        {
            slot.UnLockSlot();
        }
    }
}