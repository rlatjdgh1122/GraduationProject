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

    private SkillData m_skillData => skillUI.SkillData;

    private Dictionary<SkillType, string> dic = new();


    private void Awake()
    {
        dic.Add(SkillType.CoolTime, "초");
        dic.Add(SkillType.ActionEnter, "대");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("DSF");
        if (m_skillData == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[m_skillData.SkillType];

        ExplainUI.ShowPanel($"[스킬]", m_skillData.Name, $"{value}{unit}", m_skillData.Explain);
    }


}
