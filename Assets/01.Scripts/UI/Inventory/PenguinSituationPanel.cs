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
            UIManager.Instance.ShowWarningUI("����� HP�� �������ֽ��ϴ�");

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

        //penguin.HealthCompo.currentHealth = penguin.HealthCompo.maxHealth; //�ϴ� �ӽÿ�
        int applyHP = penguin.HealthCompo.maxHealth - penguin.HealthCompo.currentHealth;
        penguin.HealthCompo.ApplyHeal(applyHP); //�ϴ� �ӽÿ�

        UIManager.Instance.ShowWarningUI($"{data.PenguinName}�� ü���� ȸ���Ǿ����ϴ�!");

        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
