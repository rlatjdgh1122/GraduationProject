using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralSelectSlotList : MonoBehaviour
{
    public List<GeneralSelectSlot> list;

    private void OnEnable()
    {
        list = transform.GetComponentsInChildren<GeneralSelectSlot>().ToList();
    }

    private void OnDisable()
    {
        list.TryClear();
    }

    public void SetSelectedSlots(GeneralInfoDataSO info)
    {
        foreach (var slot in list)
        {
            if (slot.GeneralData.InfoData == info)
            {
                Debug.Log(slot.name);
                slot.SetSelectedPanel();
            }
                
        }
    }
}
