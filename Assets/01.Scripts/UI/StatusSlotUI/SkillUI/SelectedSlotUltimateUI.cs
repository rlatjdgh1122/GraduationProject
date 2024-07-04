using SkillSystem;
using UnityEngine;

public class SelectedSlotUltimateUI : UltimateUI
{
    public void Setting(float _currentFillAmount)
    {
        CurrentFillAmount = _currentFillAmount;
        bliendGauge.fillAmount = CurrentFillAmount;
    }

    public override void OnUltimateUsed()
    {
        base.OnUltimateUsed();

    }

    public override void OnUltimateActionEnter()
    {
        base.OnUltimateActionEnter();

        CurrentFillAmount -= 1f / Value; // value�� ���� ���� ����
        bliendGauge.fillAmount = CurrentFillAmount;
    }
}
