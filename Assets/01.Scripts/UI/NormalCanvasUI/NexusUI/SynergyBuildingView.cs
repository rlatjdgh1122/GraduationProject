using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynergyBuildingView : BuildingView
{
    protected override void Start()
    {
        _building = NexusManager.Instance.BuildingItemInfos[name];

        spawn = GetComponent<SpawnBuildingButton>();
        purchaseButton.onClick.AddListener(() => spawn.SpawnBuildingEventHandler(_building.Prefab.GetComponent<BaseBuilding>(), _building));

        UpdateUI();

        foreach (var ui in _building.NecessaryResource)
        {
            BuildingPriceUI priceUI = Instantiate(_priceUI);

            var data = ResourceManager.Instance.TypeToResourceDataSO(ui.Type);

            priceUI.UpdateUI(data.resourceIcon, ui.Count);
            priceUI.transform.SetParent(buildingPriceTrm.transform);
        }
    }

    public override void OnPurchase()
    {
        base.OnPurchase();
    }

    public override void UpdateUI()
    {
        buildingName.text = _building.Name;
        buildingIcon.sprite = _building.UISprite;
        maxInstallableCount.text = $"{_building.CurrentInstallCount}/{_building.MaxInstallableCount}";
    }

    public override void UIUpdate()
    {
        UpdateUI();
    }
}
