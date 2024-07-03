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
        Debug.Log("으악!");
        currentFillAmount -= 1f / value; // value에 따라 비율 감소
        blinedGauge.fillAmount = currentFillAmount;
    }

}
