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

        CostManager.Instance.SubtractFromCurrentCost(currentGeneralStat.InfoData.Price, () =>
        {
            PurchaseGeneral();

            Debug.Log($"����: {_currentView.dummyGeneralPenguin}");
            //���� �������ְ�
            var spawnDummy = _penguinFactory.SpawnDummyPenguinHandler(_currentView.dummyGeneralPenguin);
            //���� ������� �� ���� �ֱ�
            spawnDummy.Stat = _currentView.GeneralInfoData;
            Debug.Log($"����: {spawnDummy.Stat}");

            LegionInventoryManager.Instance.AddPenguin(general.InfoData);

            _currentView.SetUpgradeUI(currentGeneralStat);

            //if (TutorialManager.Instance.CurTutoQuestIdx == 6) //�ϴ� ����Ʈ
            //{
            //    TutorialManager.Instance.CurTutorialProgressQuest();
            //}
        });
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
        CostManager.Instance.SubtractFromCurrentCost(currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue(), () =>
        {
            ShowBoxes();
        });
    }

    private void UpgradeGeneral()
    {
        currentGeneralStat.Level++;
        CostManager.Instance.SubtractFromCurrentCost(currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue());
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
        UIManager.Instance.HidePanel("GeneralUI");
    }
}
