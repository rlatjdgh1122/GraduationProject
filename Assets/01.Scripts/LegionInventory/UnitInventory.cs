using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : InitialUnitInventory
{
    [Header("юс╫ц©о")]
    [SerializeField] EntityInfoDataSO so1;
    [SerializeField] EntityInfoDataSO so2;
    [SerializeField] EntityInfoDataSO so3;
    [SerializeField] EntityInfoDataSO so4;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
             LegionInventoryManager.Instance.AddPenguin(so1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LegionInventoryManager.Instance.AddPenguin(so2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LegionInventoryManager.Instance.AddPenguin(so3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LegionInventoryManager.Instance.AddPenguin(so4);
        }
    }
    //====================================================


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