using SkillSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using static SynergySettingData;

public class UltimateMouseEventUI : MountEventUI
{
    private SelectedSlotUltimateUI ultimateUI = null;

    private UltimateData ultimateData => ultimateUI.UltimateData;

    private Dictionary<UltimateType, string> dic = new();

    private void Awake()
    {
        dic.Add(UltimateType.WaveCount, "���̺�");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (ultimateData == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[ultimateData.UltimateType];

        ExplainUI.ShowPanel($"[�ñر�]", ultimateData.Name, $"{value}{unit}", ultimateData.Explain);
    }
}
