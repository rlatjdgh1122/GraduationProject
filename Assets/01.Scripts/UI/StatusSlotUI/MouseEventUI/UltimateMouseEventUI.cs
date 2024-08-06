using SkillSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using static SynergySettingData;

public class UltimateMouseEventUI : MountEventUI
{
    public SelectedSlotUltimateUI ultimateUI = null;

    private UltimateData ultimateData => ultimateUI.UltimateData;

    private Dictionary<UltimateType, string> dic = new();

    private void Awake()
    {
        dic.Add(UltimateType.WaveCount, "¿þÀÌºê");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (ultimateData == null) return;
        if (Army.UltimateController == null) return;

        int value = Army.UltimateController.GetMaxValue;
        string unit = dic[ultimateData.UltimateType];

        ExplainUI.ShowPanel($"[±Ã±Ø±â]", ultimateData.Name, $"{value}{unit}", ultimateData.Explain);
    }
}
