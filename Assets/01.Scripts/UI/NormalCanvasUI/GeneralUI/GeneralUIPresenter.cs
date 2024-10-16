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

        if (selectedAbility.value == 0) //아직 업그레이드 선택지가 정해져있지 않은 상황에서
            SetRandom(); //업그레이드 선택지 초기값 설정해주는 거
    }

    #region 첫 구매 관련
    public void Purchase(GeneralStat general)
    {
        currentGeneralStat = general;

        Debug.Log("구매 완료");

        CostManager.Instance.SubtractFromCurrentCost(currentGeneralStat.InfoData.Price, () =>
        {
            PurchaseGeneral();

            Debug.Log($"생성: {_currentView.dummyGeneralPenguin}");
            //더미 생성해주고
            var spawnDummy = _penguinFactory.SpawnDummyPenguinHandler(_currentView.dummyGeneralPenguin);
            //더미 펭귄한테 내 스탯 주기
            spawnDummy.Stat = _currentView.GeneralInfoData;
            Debug.Log($"스텟: {spawnDummy.Stat}");

            LegionInventoryManager.Instance.AddPenguin(general.InfoData);

            _currentView.SetUpgradeUI(currentGeneralStat);

            //if (TutorialManager.Instance.CurTutoQuestIdx == 6) //일단 퀘스트
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

    #region 업그레이드 관련
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

    #region 선택지 관련
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
