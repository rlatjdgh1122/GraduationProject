using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static SynergySettingData;

public class SynergyMouseEventUI : MountEventUI
{
    public SeletedSlotSynergyUI synergyUI = null;

    private SynergyData synergyData => synergyUI.SynergyData;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (synergyData == null) return;

        ExplainUI.ShowPanel($"[½Ã³ÊÁö]", synergyData.Name, "", synergyData.Explain);
    }
}
