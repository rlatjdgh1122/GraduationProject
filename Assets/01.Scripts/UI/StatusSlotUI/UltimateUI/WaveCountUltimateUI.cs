using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCountUltimateUI : UltimateUI
{
    public override void OnUltimateUsed()
    {
        base.OnUltimateUsed();
    }

    public override void OnUltimateActionEnter()
    {
        CurrentFillAmount -= 1f / Value; // value�� ���� ���� ����
        bliendGauge.fillAmount = CurrentFillAmount;
    }
}
