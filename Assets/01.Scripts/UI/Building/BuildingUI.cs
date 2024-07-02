using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : PopupUI
{
    [HideInInspector] public SynergyBuildingStat BuildingStat;
    [HideInInspector] public Health BuildingHealth;

    [SerializeField] private List<GameObject> _panels;

    private BuildingUIComponent[] _repairPanels;

    public override void Awake()
    {
        base.Awake();

        _repairPanels = transform.GetComponentsInChildren<BuildingUIComponent>();
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
}