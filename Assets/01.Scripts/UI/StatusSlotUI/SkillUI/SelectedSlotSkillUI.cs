using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlotSkillUI : SkillUI
{
    private SkillType skillType = SkillType.None;

    public void Setting(float _currentFillAmount, SkillType _skillType)
    {
        skillType = _skillType;
        SkillImage.sprite = image;

        CurrentFillAmount = _currentFillAmount;
        bliendGauge.fillAmount = CurrentFillAmount;
    }

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        currntValue = 0f;
    }

    public override void OnSkillActionEnter()
    {
        base.OnSkillActionEnter();

        if (skillType == SkillType.CoolTime) //��Ÿ��
        {
            currntValue += Time.deltaTime;

            if (currntValue < value)
            {
                CurrentFillAmount = 1f - (currntValue / value);
            }
        }

        else if (skillType == SkillType.ActionEnter) //�� ��
        {
            CurrentFillAmount -= 1f / value; // value�� ���� ���� ����
        }

        bliendGauge.fillAmount = CurrentFillAmount;
    }
}
