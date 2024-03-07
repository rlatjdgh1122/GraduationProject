using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPresenter : PopupUI
{
    [HideInInspector]
    public GeneralView _currentView;
    [HideInInspector]
    public GeneralStat currentGeneralStat;

    [HideInInspector]
    public Ability selectedAbility;

    #region events
    public Action OnUpdateUpgradeUI;
    #endregion

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
    }

    public void SetCurrentView(GeneralView generalView)
    {
        _currentView = generalView;
        currentGeneralStat = _currentView.generalStat;
    }

    #region ù ���� ����
    public void Purchase(GeneralStat general)
    {
        currentGeneralStat = general;
        if (currentCost >= currentGeneralStat.PenguinData.price)
        {
            PurchaseGeneral();

            currentCost -= currentGeneralStat.PenguinData.price;
            _currentView.SetUpgradeUI(currentGeneralStat);
        }
        else
        {
            Debug.Log("��ȭ���� �����Ͽ� �������� ������ �� �����ϴ�.");
        }
    }

    private void PurchaseGeneral()
    {
        currentGeneralStat.GeneralData.IsAvailable = true;
    }
    #endregion

    #region ���׷��̵� ����
    public void ShowUpgradePanel()
    {
        UIManager.Instance.ShowPanel("GeneralUpgrade");
    }    

    public void Upgrade()
    {
        if (currentCost >= currentGeneralStat.GeneralData.levelUpPrice.GetValue())
        {
            ShowBoxes();
        }
        else
        {
            Debug.Log("��ȭ���� �����Ͽ� ���׷��̵��� �� �����ϴ�.");
        }
    }

    private void UpgradeGeneral()
    {
        currentGeneralStat.Level++;
        currentCost -= currentGeneralStat.GeneralData.levelUpPrice.GetValue();
        _currentView.UpdateUpgradeUI(currentGeneralStat);
    }
    #endregion

    #region ������ ����
    public void SelectGeneralBox()
    {
        UpgradeGeneral();
        AddAbilityStat();
        HideBoxes();
    }

    public void SelectSynergyBox()
    {
        currentGeneralStat.GeneralData.synergy.level++;
        HideBoxes();
    }

    public void SetRandom()
    {
        List<Ability> statTypes = currentGeneralStat.GeneralData.abilities;

        Ability chosenStat = statTypes[UnityEngine.Random.Range(0, statTypes.Count)];
        selectedAbility = chosenStat;
    }

    public void AddAbilityStat()
    {
        //selectedAbility.baseIncreaseValue += selectedAbility.increaseValue;
        currentGeneralStat.AddStat(selectedAbility.increaseValue, selectedAbility.statType, StatMode.Increase);
    }

    public void ShowBoxes()
    {
        UIManager.Instance.ShowPanel("GeneralChoiceBox");
    }

    public void HideBoxes()
    {
        UIManager.Instance.HidePanel("GeneralChoiceBox");
    }
    #endregion

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
