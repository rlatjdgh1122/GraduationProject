using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingUI : PopupUI
{
    [HideInInspector] public SynergyBuildingStat BuildingStat;
    [HideInInspector] public SynergyBuilding SynergyBuilding;
    [HideInInspector] public Health BuildingHealth;

    [SerializeField] private List<GameObject> _panels;

    private BuildingUIComponent[] _repairPanels;

    private BuildingRepairPanel[] _repairPanel;

    public override void Awake()
    {
        base.Awake();

        _repairPanels = transform.GetComponentsInChildren<BuildingUIComponent>();
        _repairPanel = transform.GetComponentsInChildren<BuildingRepairPanel>();
    }

    public void SetStat()
    {
        if (BuildingStat.BuildingType == SynergyBuildingType.KatanaSynergyBuilding) //이 또한 매우 끔찍한 방법이니 나중에 고치도록 하자.
        {
            _panels.ForEach(p => p.SetActive(false));
            _panels[0].SetActive(true);
        }
        else if (BuildingStat.BuildingType == SynergyBuildingType.PoliceSynergyBuilding)
        {
            _panels.ForEach(p => p.SetActive(false));
            _panels[1].SetActive(true);
        }
        else if (BuildingStat.BuildingType == SynergyBuildingType.LanceSynergyBuilding)
        {
            _panels.ForEach(p => p.SetActive(false));
            _panels[2].SetActive(true);
        }

        foreach (BuildingUIComponent panel in _repairPanels)
        {
            panel.buildingStat = BuildingStat;
            panel.synergyBuilding = SynergyBuilding;
            panel.buildingHealth = BuildingHealth;
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public void HideBuildingUI()
    {
        //UIManager.Instance.HidePanel("BuildingUI");
        HidePanel();

        UIManager.Instance.ResetPanel();
    }
}