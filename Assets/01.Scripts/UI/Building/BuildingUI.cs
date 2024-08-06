using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuildingUI : PopupUI
{
    public Health BuildingHealth { get; set; }
    public SynergyBuilding SynergyBuilding { get; set; }
    public SynergyBuildingInfoDataSO InfoData { get; private set; }

    private BuildingUIComponent[] _repairPanels;

    private BuildingUpgrade _buildingUpgrade;
    private BuildingRepairPanel _buildingRepairPanel;

    public override void Awake()
    {
        base.Awake();

        _repairPanels = transform.GetComponentsInChildren<BuildingUIComponent>();
        _buildingUpgrade = transform.GetComponentInChildren<BuildingUpgrade>();
        _buildingRepairPanel = transform.GetComponentInChildren<BuildingRepairPanel>();
    }

    public void SetStat()
    {
        foreach (BuildingUIComponent panel in _repairPanels)
        {
            panel.buildingHealth  = BuildingHealth;
            panel.synergyBuilding = SynergyBuilding;
        }
    }

    public void ShowBuildingUI(SynergyBuildingInfoDataSO infoData)
    {
        InfoData = infoData;

        UIManager.Instance.ShowPanel("BuildingUI");

        _buildingUpgrade.InitSlot(infoData);
        _buildingRepairPanel.SetBuildingInfo(infoData);

        _buildingUpgrade.ShowPanel();
    }

    public void HideBuildingUI()
    {
        HidePanel();

        UIManager.Instance.ResetPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _buildingRepairPanel.HidePanel();
        _buildingUpgrade.ClosePanel();
        _buildingUpgrade.OnMovePanel(0);
        _buildingRepairPanel.OnMovePanel(650);
    }
}