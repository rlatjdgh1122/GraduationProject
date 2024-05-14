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
    protected bool canBuy;
    protected bool isReducedHP;

    protected bool isHeal;
    protected bool isRetire;


    public void CheckBuy(float percent, int penguinPrice)
    {
        hpPercent = (int)(percent * 100);

        price = penguinPrice - (int)StatCalculator.Percent(penguinPrice, hpPercent);
        canBuy = CostManager.Instance.CheckRemainingCost(price);
        isReducedHP = percent < 1;

        if (canBuy && isReducedHP)
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
        if(!canBuy)
        {
            UIManager.Instance.ShowWarningUI("재화가 부족합니다!");
            return;
        }

        if (!canClick)
        {
            UIManager.Instance.ShowWarningUI("펭귄의 HP가 가득차있습니다");
            return;
        }
        
        if (isHeal)
        {
            HealEvent();
        }
        else if (isRetire)
        {
            RetireEvent();
        }

        HidePanel();

        ResetPanel();
    }

    private void ResetPanel()
    {
        isHeal = false;
        isRetire = false;
        canClick = false;
        data = null;
    }

    public void RetireEvent()
    {
        if (!isRetire) return;

        LegionInventoryManager.Instance.DeadLegionPenguin(data.LegionName, data.SlotIdx, true);

        var dummy = PenguinManager.Instance.GetDummyByInfoData(data);
        PenguinManager.Instance.RemoveDummyPenguin(dummy);
        PoolManager.Instance.Push(dummy);

        LegionInventoryManager.Instance.SaveLegion();
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void HealEvent()
    {
        if (!isHeal) return;

        var penguin = PenguinManager.Instance.GetPenguinByInfoData(data);

        int applyHP = penguin.HealthCompo.maxHealth - penguin.HealthCompo.currentHealth;
        penguin.HealthCompo.ApplyHeal(applyHP); //일단 임시완

        UIManager.Instance.ShowWarningUI($"{data.PenguinName}의 체력이 회복되었습니다!");
        CostManager.Instance.SubtractFromCurrentCost(price);
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
