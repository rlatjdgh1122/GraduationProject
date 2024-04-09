using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIPresenter : PopupUI
{
    #region events
    public Action OnUpdateUpgradeUI;
    #endregion

    [HideInInspector]
    public GeneralView _currentView;
    [HideInInspector]
    public GeneralStat currentGeneralStat;
    [HideInInspector]
    public Ability selectedAbility;

    private DummyPenguinFactory _penguinFactory;

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

        _penguinFactory = GameObject.Find("PenguinSpawner/DummyPenguinFactory").GetComponent<DummyPenguinFactory>();
    }

    public void SetCurrentView(GeneralView generalView)
    {
        _currentView = generalView;
        currentGeneralStat = _currentView.GeneralInfoData;

        if (selectedAbility.increaseValue == 0) //���� ���׷��̵� �������� ���������� ���� ��Ȳ����
            SetRandom(); //���׷��̵� ������ �ʱⰪ �������ִ� ��
    }

    #region ù ���� ����
    public void Purchase(GeneralStat general)
    {
        currentGeneralStat = general;

        if (currentCost >= currentGeneralStat.InfoData.Price)
        {
            PurchaseGeneral();

            _penguinFactory.SpawnDummyPenguinHandler(_currentView.dummyGeneralPenguin);
            LegionInventoryManager.Instance.AddPenguin(general.InfoData);

            currentCost -= currentGeneralStat.InfoData.Price;
            _currentView.SetUpgradeUI(currentGeneralStat);

            //if (TutorialManager.Instance.CurTutoQuestIdx == 6) //�ϴ� ����Ʈ
            //{
            //    TutorialManager.Instance.CurTutorialProgressQuest();
            //}
        }
        else
        {
            Debug.Log("��ȭ���� �����Ͽ� �������� ������ �� �����ϴ�.");
        }
    }

    private void PurchaseGeneral()
    {
        currentGeneralStat.GeneralDetailData.IsAvailable = true;
    }
    #endregion

    #region ���׷��̵� ����
    public void ShowUpgradePanel()
    {
        UIManager.Instance.ShowPanel("GeneralUpgrade");
    }    

    public void Upgrade()
    {
        if (currentCost >= currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue())
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
        currentCost -= currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue();
        _currentView.UpdateUpgradeUI(currentGeneralStat);
    }
    #endregion

    #region ������ ����
    public void SelectGeneralBox()
    {
        UpgradeGeneral();
        AddAbilityStat();
        SetRandom();
        HideBoxes();
    }

    public void SelectSynergyBox()
    {
        currentGeneralStat.GeneralDetailData.synergy.level++;
        SetRandom();
        HideBoxes();
    }

    public void SetRandom()
    {
        List<Ability> statTypes = currentGeneralStat.GeneralDetailData.abilities;

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
        UIManager.Instance.HidePanel("GeneralUpgrade");
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
