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
        dic.Add(UltimateType.WaveCount, "웨이브");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ExplainUI.ShowPanel($"[궁극기]", "잠겨있음", $"???", "군단장과 어울리는 펭귄을 조합하여 궁극기를 해금해보세요!");

        if (ultimateData == null) return;
        if (Army.UltimateController == null) return;

        int value = Army.UltimateController.GetMaxValue;
        string unit = dic[ultimateData.UltimateType];

        ExplainUI.ShowPanel($"[궁극기]", ultimateData.Name, $"{value}{unit}", ultimateData.Explain);
    }
}
