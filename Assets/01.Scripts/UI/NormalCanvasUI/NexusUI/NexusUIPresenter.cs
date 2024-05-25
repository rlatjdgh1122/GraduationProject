using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NexusUIPresenter : NexusPopupUI
{
    [HideInInspector]
    public BuildingType buildingType;

    private BuildingFactory _buildingFactory;
    private List<NexusPopupUI> _receiverList;

    private NexusUpgradePanel _nexusUpgradePanel;

    public override void Awake()
    {
        base.Awake();

        _buildingFactory = FindAnyObjectByType<BuildingFactory>();
        _nexusUpgradePanel = FindObjectOfType<NexusUpgradePanel>();
        _receiverList = GetComponentsInChildren<NexusPopupUI>().ToList();
    }

    #region NexusUI
    public void LevelUp()
    {
        if (!CostManager.Instance.CheckRemainingCost(_nexusStat.upgradePrice))
        {
            UIManager.Instance.ShowWarningUI($"재화가 부족합니다!");
            return;
        }

        _nexusStat.maxHealth.AddSum
            (_nexusStat.maxHealth.GetValue(), _nexusStat.level, _nexusStat.levelupIncreaseValue);
        _nexusStat.level++;

        NoiseManager.Instance.IncreaseMaxNoise(_nexusStat.level);

        foreach (BuildingItemInfo building in NexusManager.Instance.BuildingItemInfos.Values)
        {
            if (_nexusStat.level == building.UnlockedLevel)
            {
                building.IsUnlocked = true;
            }
        }

        LegionInventoryManager.Instance.AddLegionGeneralPenguinIn5Wave++; //지워야함 무조건

        CostManager.Instance.SubtractFromCurrentCost(_nexusStat.upgradePrice);

        _nexusStat.upgradePrice *= 1.5f; // <-이건 임시
        WorkerManager.Instance.MaxWorkerCount++; //이것도 임시수식
        SoundManager.Play2DSound(SoundName.LevelUp); //이것도 임시

        NexusManager.Instance.SetNexusHealth();        
        NexusManager.Instance.UpdateNexusInfoData();

        _nexusUpgradePanel.ShowPanel();
        UpdateRecieverUI();

        if (TutorialManager.Instance.CurTutoQuestIdx == 4) //일단 퀘스트
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);        
        }
    }

    public void UpdateRecieverUI()
    {
        foreach (NexusPopupUI _receiver in _receiverList)
        {
            _receiver.UIUpdate();
        }
    }

    public void OnAdmitBuildingPanel()
    {
        UIManager.Instance.ShowPanel("BuildingPanel", true);
        UIManager.Instance.HidePanel("NexusPanel");
        //UIManager.Instance.MovePanel("NexusPanel", -2500, 0, 0.7f);
        //UIManager.Instance.MovePanel("BuildingPanel", 0, 0, 0.7f);
    }

    public void OnAdmitNexusPanel()
    {
        UIManager.Instance.ShowPanel("NexusPanel", true);
        UIManager.Instance.HidePanel("BuildingPanel");
        //UIManager.Instance.MovePanel("NexusPanel", 0, 0, 0.7f);
        //UIManager.Instance.MovePanel("BuildingPanel", 2500, 0, 0.7f);
    }
    #endregion

    #region buildingUI
    public void PurchaseBuilding(BuildingView view)
    {
        foreach (var resource in view.Building.NecessaryResource)
        {
            ResourceManager.Instance.resourceDictionary.TryGetValue(resource.NecessaryResource.resourceData, out var saveResource);

            if (saveResource == null || saveResource.stackSize < resource.NecessaryResourceCount)
            {
                UIManager.Instance.ShowWarningUI("자원이 부족합니다!");
                return;
            }
        }

        if (view.Building.IsUnlocked)
        {
            view.spawn.SetUpButtonInfo(view.purchaseButton, _buildingFactory, view.Building);
        }
        else
        {
            UIManager.Instance.ShowWarningUI($"자원이 부족합니다");
        }
    }
    #endregion


    public override void HidePanel()
    {
        base.HidePanel();

        NexusManager.Instance.CanClick = false;
    }

    public void HideNexusPanel()
    {
        UIManager.Instance.HidePanel("NexusUI");
    }

    public override void UIUpdate()
    {

    }
}
