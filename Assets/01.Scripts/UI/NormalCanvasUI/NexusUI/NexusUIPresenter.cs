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
            UIManager.Instance.ShowWarningUI($"��ȭ�� �����մϴ�!");
            return;
        }

        _nexusStat.maxHealth.AddSum
            (_nexusStat.maxHealth.GetValue(), _nexusStat.level, _nexusStat.levelupIncreaseValue);
        _nexusStat.level++;

        NoiseManager.Instance.IncreaseMaxNoise(_nexusStat.level);

        foreach (BuildingItemInfo building in _buildingDatabase.BuildingItems)
        {
            if (_nexusStat.level == building.UnlockedLevel)
            {
                building.IsUnlocked = true;
            }
        }

        CostManager.Instance.SubtractFromCurrentCost(_nexusStat.upgradePrice);

        _nexusStat.upgradePrice *= 2; // <-�̰� �ӽ�
        WorkerManager.Instance.MaxWorkerCount++; //�̰͵� �ӽü���
        SoundManager.Play2DSound(SoundName.LevelUp); //�̰͵� �ӽ�

        NexusManager.Instance.SetNexusHealth();        
        NexusManager.Instance.UpdateNexusInfoData();

        _nexusUpgradePanel.ShowPanel();
        UpdateRecieverUI();

        if (TutorialManager.Instance.CurTutoQuestIdx == 4) //�ϴ� ����Ʈ
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);        
        }
    }

    public void UpdateRecieverUI()
    {
        _receiverList.ForEach(r => r.UIUpdate());
    }

    public void OnAdmitBuildingPanel()
    {
        UIManager.Instance.MovePanel("NexusPanel", -2500, 0, 0.7f);
        UIManager.Instance.MovePanel("BuildingPanel", 0, 0, 0.7f);
    }

    public void OnAdmitNexusPanel()
    {
        UIManager.Instance.MovePanel("NexusPanel", 0, 0, 0.7f);
        UIManager.Instance.MovePanel("BuildingPanel", 2500, 0, 0.7f);
    }
    #endregion

    #region buildingUI
    public void PurchaseBuilding(BuildingView view)
    {
        foreach (var resource in view.building.NecessaryResource)
        {
            ResourceManager.Instance.resourceDictionary.TryGetValue(resource.NecessaryResource.resourceData, out var saveResource);

            if (saveResource == null || saveResource.stackSize < resource.NecessaryResourceCount)
            {
                UIManager.Instance.ShowWarningUI("�ڿ��� �����մϴ�!");
                return;
            }
        }

        if (view.building.IsUnlocked)
        {
            view.spawn.SetUpButtonInfo(view.purchaseButton, _buildingFactory, view.building);
        }
        else
        {
            UIManager.Instance.ShowWarningUI($"�ڿ��� �����մϴ�");
        }
    }
    #endregion

    public void HideNexusPanel()
    {
        UIManager.Instance.HidePanel("NexusUI");
    }

    public override void UIUpdate()
    {

    }
}
