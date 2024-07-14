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
        dic.Add(SkillType.CoolTime, "��");
        dic.Add(SkillType.ActionEnter, "��");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (skillData == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[skillData.SkillType];

        ExplainUI.ShowPanel($"[��ų]", skillData.Name, $"{value}{unit}", skillData.Explain);
    }


}
