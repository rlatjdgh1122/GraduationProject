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
        currentFillAmount -= 1f / value; // value�� ���� ���� ����
        bliendGauge.fillAmount = currentFillAmount;
    }
}
