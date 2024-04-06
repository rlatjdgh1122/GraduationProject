using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class LegionChange : InitLegionChange
{
    private LegionBuyPanel _buyPanel;

    public override void Awake()
    {
        base.Awake();

        _buyPanel = transform.Find("BuyLegionPanel").GetComponent<LegionBuyPanel>();
    }

    public void ClickLegionChangeButton(int LegionNumber)
    {
        if (legion.LegionList()[LegionNumber].Locked) //������ ����ִٸ�
        {
            BuyLegion();
        }

        if(legion.LegionInven.ChangedInCurrentLegion()) // ���� ���ܿ� �ٲ�� �ִٸ�
        {

        }

        legion.LegionInven.ChangeLegion(legion.LegionName(LegionNumber));
    }

    public void BuyLegion()
    {
        _buyPanel.ShowPanel();
    }
}