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
        dic.Add(SkillType.CoolTime, "��");
        dic.Add(SkillType.ActionEnter, "��");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ExplainUI.ShowPanel($"[��ų]", "�������", $"???", "�������� �߰��Ͽ� ��ų�� �ر��غ�����!");

        if (skillData == null) return;
        if (Army.SkillController == null) return;

        int value = Army.SkillController.GetMaxValue;
        string unit = dic[skillData.SkillType];

        ExplainUI.ShowPanel($"[��ų]", skillData.Name, $"{value}{unit}", skillData.Explain);
    }
}
