using DG.Tweening;
using StatOperator;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenguinSituationPanel : PopupUI
{
    protected EntityInfoDataSO data;

    protected int hpPercent;
    protected int price;

    protected bool canClick;

    protected bool canHeal;
    protected bool canRetire;


    public void CheckBuy(float percent, int penguinPrice)
    {
        hpPercent = (int)(percent * 100);

        price = penguinPrice - (int)StatCalculator.Percent(penguinPrice, hpPercent);

        if (CostManager.Instance.CheckRemainingCost(price))
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }
    }

    public void SituationEvent()
    {
        if (!canClick) return;

        if (canHeal)
        {

        }
        else if (canRetire)
        {
            RetireEvent();
        }
    }

    public void RetireEvent()
    {
        if (!canRetire) return;

        LegionInventoryManager.Instance.DeadLegionPenguin(data, data.LegionName, data.SlotIdx);

        HidePanel();

        canHeal   = false;
        canRetire = false;
        canClick  = false;

        data = null;
    }
}
