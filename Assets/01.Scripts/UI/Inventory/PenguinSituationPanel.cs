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
            UIManager.Instance.ShowWarningUI("펭귄의 HP가 가득차있습니다");

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

        
        canHeal = false;
        canRetire = false;
        canClick = false;
        data = null;
    }

    public void RetireEvent()
    {
        if (!canRetire) return;

        LegionInventoryManager.Instance.DeadLegionPenguin(data.LegionName, data.SlotIdx, true);
        var dummy = PenguinManager.Instance.GetDummyByInfoData(data);
        PenguinManager.Instance.RemoveDummyPenguin(dummy);
        PoolManager.Instance.Push(dummy);
    }

    public void HealEvent()
    {
        if (!canHeal) return;

        var penguin = PenguinManager.Instance.GetPenguinByInfoData(data);

        //penguin.HealthCompo.currentHealth = penguin.HealthCompo.maxHealth; //일단 임시완
        int applyHP = penguin.HealthCompo.maxHealth - penguin.HealthCompo.currentHealth;
        penguin.HealthCompo.ApplyHeal(applyHP); //일단 임시완

        UIManager.Instance.ShowWarningUI($"{data.PenguinName}의 체력이 회복되었습니다!");

        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
