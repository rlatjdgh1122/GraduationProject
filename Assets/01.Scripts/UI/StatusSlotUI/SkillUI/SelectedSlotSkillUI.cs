using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlotSkillUI : SkillUI
{
    private SkillType skillType = SkillType.None;

    public void Setting(float _currentValue, float _currentFillAmount, SkillType _skillType)
    {
        skillType = _skillType;
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

        if (skillType == SkillType.CoolTime) //쿨타임
        {
            CurrntValue += Time.deltaTime;

            if (CurrntValue < value)
            {
                CurrentFillAmount = 1f - (CurrntValue / value);
            }
        }

        else if (skillType == SkillType.ActionEnter) //그 외
        {
            CurrentFillAmount -= 1f / value; // value에 따라 비율 감소
        }

        bliendGauge.fillAmount = CurrentFillAmount;
    }
}
