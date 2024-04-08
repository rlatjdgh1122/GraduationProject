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

        if (selectedAbility.increaseValue == 0) //아직 업그레이드 선택지가 정해져있지 않은 상황에서
            SetRandom(); //업그레이드 선택지 초기값 설정해주는 거
    }

    #region 첫 구매 관련
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

            //if (TutorialManager.Instance.CurTutoQuestIdx == 6) //일단 퀘스트
            //{
            //    TutorialManager.Instance.CurTutorialProgressQuest();
            //}
        }
        else
        {
            Debug.Log("통화량이 부족하여 아이템을 구매할 수 없습니다.");
        }
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
        if (currentCost >= currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue())
        {
            ShowBoxes();
        }
        else
        {
            Debug.Log("통화량이 부족하여 업그레이드할 수 없습니다.");
        }
    }

    private void UpgradeGeneral()
    {
        currentGeneralStat.Level++;
        currentCost -= currentGeneralStat.GeneralDetailData.levelUpPrice.GetValue();
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
