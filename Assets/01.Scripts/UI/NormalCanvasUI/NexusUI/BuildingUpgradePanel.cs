using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradePanel : NexusPopupUI
{
    public TextMeshProUGUI buildingCategory;
    public TextMeshProUGUI buildingLevel;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public TextMeshProUGUI buildingDescription;
    public TextMeshProUGUI needResource;

    private BuildingItemInfo _building;

    public override void Awake()
    {
        base.Awake();
    }

    public void SetBuilding(BuildingView view)
    {
        _building = view.building;
    }

    public void Upgrade()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        buildingCategory.text = $"{_building.BuildingTypeEnum}";
        buildingLevel.text = $"Lv {_building.Level}";
        buildingName.text = $"{_building.Name}";
        buildingIcon.sprite = _building.UISprite;
        buildingDescription.text = _building.Description;
        needResource.text = $"{_building.NecessaryResourceCount}";
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UpdateUI();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
