using SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GeneralSettingData;

public class SkillMouseEventUI : MountEventUI
{
    private SelectedSlotSkillUI skillUI = null;

    private SkillData skillData => skillUI.SkillData;

    private Dictionary<SkillType, string> dic = new();


    private void Awake()
    {
        dic.Add(SkillType.CoolTime, "초");
        dic.Add(SkillType.ActionEnter, "대");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (skillData == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[skillData.SkillType];

        ExplainUI.ShowPanel($"[스킬]", skillData.Name, $"{value}{unit}", skillData.Explain);
    }


}
