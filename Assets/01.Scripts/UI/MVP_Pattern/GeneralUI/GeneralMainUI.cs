using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralMainUI : PopupUI
{
    public GeneralUpgrade generalUpgradeUI;
    public GeneralView _generalView;
    public GameObject contentBox;

    [HideInInspector] 
    public GeneralStat currentGeneralStat;

    private int currentCost
    {
        get
        {
            return CostManager.Instance.Cost;
        }
        set
        {
            CostManager.Instance.Cost = value;
        }
    }

    public override void Awake()
    {
        base.Awake();
        
        //_generalViewList = contentBox.GetComponentsInChildren<GeneralView>().ToList();
    }

    public void SetGeneralView(GeneralView generalView)
    {
        _generalView = generalView;
        currentGeneralStat = _generalView.generalStat;
    }

    public void Purchase(GeneralStat general)
    {
        currentGeneralStat = general;
        if (currentCost >= currentGeneralStat.PenguinData.price)
        {
            PurchaseGeneral();

            currentCost -= currentGeneralStat.PenguinData.price;
            _generalView.SetUpgradeUI(currentGeneralStat);
        }
        else
        {
            Debug.Log("통화량이 부족하여 아이템을 구매할 수 없습니다.");
        }
    }

    public void ShowUpgradePanel(GeneralStat general)
    {
        generalUpgradeUI.ShowPanel();
    }

    public void Upgrade()
    {
        if (currentCost >= currentGeneralStat.GeneralData.levelUpPrice.GetValue())
        {
            UpgradeGeneral();

            currentCost -= currentGeneralStat.GeneralData.levelUpPrice.GetValue();
            _generalView.UpdateUpgradeUI(currentGeneralStat);
        }
        else
        {
            Debug.Log("통화량이 부족하여 업그레이드할 수 없습니다.");
        }
    }

    private void PurchaseGeneral()
    {
        currentGeneralStat.GeneralData.IsAvailable = true;
    }

    private void UpgradeGeneral()
    {
        currentGeneralStat.Level++;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ExitButton()
    {
        base.ExitButton();
    }
}
