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

    public override void Awake()
    {
        base.Awake();

        _repairPanels = transform.GetComponentsInChildren<BuildingUIComponent>();
    }

    public void SetStat()
    {
        foreach (BuildingUIComponent panel in _repairPanels)
        {
            panel.infoData        = InfoData;
            panel.buildingHealth  = BuildingHealth;
            panel.synergyBuilding = SynergyBuilding;
        }
    }

    public void ShowBuildingUI(SynergyBuildingInfoDataSO infoData)
    {
        InfoData = infoData;

        UIManager.Instance.ShowPanel("BuildingUI");
    }

    public void HideBuildingUI()
    {
        HidePanel();

        UIManager.Instance.ResetPanel();
    }
}