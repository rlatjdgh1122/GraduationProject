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
    //[HideInInspector]
    public GeneralStat currentGeneralStat;
    [HideInInspector]
    public Ability selectedAbility;

    private DummyPenguinFactory _penguinFactory;

    private bool _buyGeneral = false;//���� ������ ��������

    private float currentCost
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

        if (selectedAbility.value == 0) //���� ���׷��̵� �������� ���������� ���� ��Ȳ����
            SetRandom(); //���׷��̵� ������ �ʱⰪ �������ִ� ��
    }

    #region ù ���� ����
    public void Purchase(GeneralStat general)
    {
        currentGeneralStat = general;

        Debug.Log("���� �Ϸ�");

        if (currentCost >= currentGeneralStat.InfoData.Price)
        {
            PurchaseGeneral();

            Debug.Log($"����: {_currentView.dummyGeneralPenguin}");
            //���� �������ְ�
            var spawnDummy = _penguinFactory.SpawnDummyPenguinHandler(_currentView.dummyGeneralPenguin);
            //���� ������� �� ���� �ֱ�
            spawnDummy.Stat = _currentView.GeneralInfoData;
            Debug.Log($"����: {spawnDummy.Stat}");
            _buyGeneral = true;
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
            UIManager.Instance.ShowWarningUI($"��ȭ�� �����մϴ�!");
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
            UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�");
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
        UpgradeGeneral();
        //SetRandom();
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
        currentGeneralStat.AddStat(selectedAbility.value, selectedAbility.statType, StatMode.Increase);
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

    public void HideGeneralUI()
    {
        if(!_buyGeneral) //���� ������ ��������
        {
            UIManager.Instance.ShowWarningUI("�������� ������ �ּ���");
            return;
        }

        UIManager.Instance.HideAllPanel();
    }
}
