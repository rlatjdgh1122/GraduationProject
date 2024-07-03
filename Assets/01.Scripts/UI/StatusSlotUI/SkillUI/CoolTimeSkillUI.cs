using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTimeSkillUI : SkillUI
{
    public override void OnSkillUsed()
    {
        base.OnSkillUsed();

        currntValue = 0f;
    }

    public override void OnSkillActionEnter()
    {
        base.OnSkillActionEnter();

        currntValue += Time.deltaTime;
        if (currntValue < value)
        {
            CurrentFillAmount = 1f - (currntValue / value);
            bliendGauge.fillAmount = CurrentFillAmount;

        }//end if
    }
}