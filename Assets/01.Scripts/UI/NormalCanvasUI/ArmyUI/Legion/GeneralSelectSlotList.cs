using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralSelectSlotList : MonoBehaviour
{
    public GeneralSelectSlot[] list;

    private void Awake()
    {
        list = transform.GetComponentsInChildren<GeneralSelectSlot>();
    }

    public void SetSelectedSlots(GeneralInfoDataSO info)
    {
        foreach (var slot in list)
        {
            if (slot.GeneralData.InfoData == info)
                slot.SetSelectedPanel();
            else
                slot.ReverseSelectedPanel();
        }
    }
}
