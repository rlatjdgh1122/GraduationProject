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

        CostManager.Instance.SubtractFromCurrentCost(_nexusStat.upgradePrice);

        _nexusStat.upgradePrice *= 1.5f; // <-이건 임시
        WorkerManager.Instance.MaxWorkerCount++; //이것도 임시수식
        SoundManager.Play2DSound(SoundName.LevelUp); //이것도 임시

        NexusManager.Instance.SetNexusHealth();        
        NexusManager.Instance.UpdateNexusInfoData();

        _nexusUpgradePanel.ShowPanel();
        UpdateRecieverUI();
    }

    public void UpdateRecieverUI()
    {
        foreach (NexusPopupUI _receiver in _receiverList)
        {
            _receiver.UIUpdate();
        }
    }

    public void OnAdmitDefenseBuildingPanel()
    {
        UIManager.Instance.ShowPanel("DefenceBuildingPanel");

        UIManager.Instance.MovePanel("NexusPanel", -2500, 0, 0.7f);
        UIManager.Instance.MovePanel("SynergyBuildingPanel", 2500, 0, 0.7f);
        UIManager.Instance.MovePanel("DefenceBuildingPanel", 0, 0, 0.7f);
    }

    public void OnAdmitNexusPanel()
    {
        UIManager.Instance.ShowPanel("NexusPanel");

        UIManager.Instance.MovePanel("DefenceBuildingPanel", 2500, 0, 0.7f);
        UIManager.Instance.MovePanel("SynergyBuildingPanel", 5000, 0, 0.7f);
        UIManager.Instance.MovePanel("NexusPanel", 0, 0, 0.7f);
    }

    public void OnAdmitSynergyBuildingPanel()
    {
        UIManager.Instance.ShowPanel("SynergyBuildingPanel");

        UIManager.Instance.MovePanel("NexusPanel", -2500, 0, 0.7f);
        UIManager.Instance.MovePanel("DefenceBuildingPanel", 2500, 0, 0.7f);
        UIManager.Instance.MovePanel("SynergyBuildingPanel", 0, 0, 0.7f);
    }
    #endregion

    #region buildingUI
    public void PurchaseBuilding(BuildingView view)
    {
        if (!ResourceManager.Instance.CheckAllResources(view.Building.NecessaryResource))
        {
            UIManager.Instance.ShowWarningUI("자원이 부족합니다!");
            return;
        }

        if (view.Building.IsUnlocked)
        {
            view.spawn.SetUpButtonInfo(view.purchaseButton, _buildingFactory, view.Building);
        }
        else
        {
            UIManager.Instance.ShowWarningUI($"잠겨있습니다!");
        }
    }
    #endregion

    public override void ShowPanel()
    {
        base.ShowPanel();

        OnAdmitNexusPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public void HideNexusPanel()
    {
        UIManager.Instance.HidePanel("NexusUI");
        UIManager.Instance.HideAllPanel();
    }

    public override void UIUpdate()
    {

    }
}
