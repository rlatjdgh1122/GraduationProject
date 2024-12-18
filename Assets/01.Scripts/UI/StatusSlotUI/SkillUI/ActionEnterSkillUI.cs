using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnterSkillUI : SkillUI
{

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();
    }

    public override void OnSkillActionEnter()
    {
        CurrentFillAmount -= 1f / value; // value에 따라 비율 감소
        bliendGauge.fillAmount = CurrentFillAmount;
    }

}
