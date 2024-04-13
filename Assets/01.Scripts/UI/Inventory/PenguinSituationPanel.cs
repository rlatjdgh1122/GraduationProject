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

        if (CostManager.Instance.CheckRemainingCost(price) && percent < 1)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }
    }

    public void SituationButtonEvent()
    {
        if (!canClick)
        {
            UIManager.Instance.ShowWarningUI("Æë±ÏÀÇ HP°¡ °¡µæÂ÷ÀÖ½À´Ï´Ù");

            return;
        }
        
        if (canHeal)
        {
            HealEvent();
        }
        else if (canRetire)
        {
            RetireEvent();
        }

        HidePanel();
    }

    public void RetireEvent()
    {
        if (!canRetire) return;

        LegionInventoryManager.Instance.DeadLegionPenguin(data.LegionName, data.SlotIdx, true);

        canHeal   = false;
        canRetire = false;
        canClick  = false;

        data = null;
    }

    public void HealEvent()
    {
        //Æë±Ï HP È¸º¹½ÃÅ°±â
    }
}
