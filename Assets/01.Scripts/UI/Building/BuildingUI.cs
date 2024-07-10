using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingUI : PopupUI
{
    public Health BuildingHealth { get; set; }
    public SynergyBuilding SynergyBuilding { get; set; }
    public SynergyBuildingInfoDataSO InfoData { get; private set; }

    private BuildingUIComponent[] _repairPanels;

    private BuildingUpgrade _buildingUpgrade;

    public override void Awake()
    {
        base.Awake();

        _repairPanels = transform.GetComponentsInChildren<BuildingUIComponent>();
        _buildingUpgrade = transform.GetComponentInChildren<BuildingUpgrade>();
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
        _buildingUpgrade.ShowPanel();
        _buildingUpgrade.InitSlot(infoData);
    }

    public void HideBuildingUI()
    {
        HidePanel();

        UIManager.Instance.ResetPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _buildingUpgrade.ClosePanel();
    }
}