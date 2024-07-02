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
        currentFillAmount -= 1f / value; // value�� ���� ���� ����
        blinedGauge.fillAmount = currentFillAmount;
    }

}
