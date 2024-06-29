using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnterSkillUI : SkillUI
{

    private void Start()
    {
        OnChangedMaxValue(5);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) OnChangedMaxValue(value - 1);
        if (Input.GetKeyDown(KeyCode.K)) OnSkillUsed();
        if (Input.GetKeyDown(KeyCode.L)) OnSkillActionEnter();
    }

    public override void OnSkillUsed()
    {
        base.OnSkillUsed();
    }

    public override void OnSkillActionEnter()
    {
        currentFillAmount -= 1f / value; // value에 따라 비율 감소
        blinedGauge.fillAmount = currentFillAmount;
    }

}
