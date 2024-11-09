using SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GeneralSettingData;

public class SkillMouseEventUI : MountEventUI
{
    public SelectedSlotSkillUI skillUI = null;

    private SkillData skillData => skillUI.SkillData;

    private Dictionary<SkillType, string> dic = new();


    private void Awake()
    {
        dic.Add(SkillType.CoolTime, "초");
        dic.Add(SkillType.ActionEnter, "대");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ExplainUI.ShowPanel($"[스킬]", "잠겨있음", $"???", "군단장을 추가하여 스킬을 해금해보세요!");

        if (skillData == null) return;
        if (Army.SkillController == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[skillData.SkillType];

        ExplainUI.ShowPanel($"[스킬]", skillData.Name, $"{value}{unit}", skillData.Explain);
    }
}
