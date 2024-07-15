using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SynergySettingData;

public class SeletedSlotSynergyUI : MonoBehaviour
{
    public SynergyData SynergyData { get; set; } = null;

    public void SetData(SynergyData data)
    {
        SynergyData = data;
    }
}
