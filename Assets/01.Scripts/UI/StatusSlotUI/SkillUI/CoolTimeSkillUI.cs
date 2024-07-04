using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{
    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        CurrntValue = 0f;
    }

    public override void OnSkillActionEnter()
    {
        base.OnSkillActionEnter();

        CurrntValue += Time.deltaTime;
        if (CurrntValue < value)
        {
            CurrentFillAmount = 1f - (CurrntValue / value);
            bliendGauge.fillAmount = CurrentFillAmount;

        }//end if
    }

    public override void OnSkillReady()
    {
        base.OnSkillReady();

        CurrntValue = value;
        CurrentFillAmount = 0f;
        bliendGauge.fillAmount = CurrentFillAmount;
    }
}