using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralSettingData;

public class SelectedSlotSkillUI : SkillUI
{
    private SkillType skillType = SkillType.None;

    public SkillData SkillData { get; set; } = null;

    public void SetData(SkillData data)
    {
        SkillData = data;
    }

    public void Setting(float _currentValue, float _currentFillAmount)
    {
        skillType = SkillData.SkillType;
        CurrntValue = _currentValue;
        CurrentFillAmount = _currentFillAmount;
        bliendGauge.fillAmount = CurrentFillAmount;
    }

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        CurrntValue = 0f;
    }

    public override void OnSkillActionEnter()
    {
        base.OnSkillActionEnter();

        if (skillType == SkillType.CoolTime) //��Ÿ��
        {
            CurrntValue += Time.deltaTime;

            if (CurrntValue < value)
            {
                CurrentFillAmount = 1f - (CurrntValue / value);
            }
        }

        else if (skillType == SkillType.ActionEnter) //�� ��
        {
            CurrentFillAmount -= 1f / value; // value�� ���� ���� ����
        }

        bliendGauge.fillAmount = CurrentFillAmount;
    }

    public override void OnSkillReady()
    {
        base.OnSkillReady();

        CurrntValue = value;
        CurrentFillAmount = 0f;
        bliendGauge.fillAmount = CurrentFillAmount;
    }
}
