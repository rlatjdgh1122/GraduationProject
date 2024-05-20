using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : NexusPopupUI
{
    [SerializeField]
    private BuildingPriceUI _priceUI;

    [HideInInspector]
    public SpawnBuildingButton spawn;
    [HideInInspector]
    public BuildingItemInfo building;

    public Button purchaseButton;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public Transform buildingPriceTrm;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        building = _buildingDatabase.BuildingItems.FirstOrDefault(building => building.CodeName == this.name);
        building.CurrentInstallCount = 0;
        spawn = GetComponent<SpawnBuildingButton>();
        purchaseButton.onClick.AddListener(() => spawn.SpawnBuildingEventHandler(building.Prefab.GetComponent<BaseBuilding>(), building));

        UpdateUI();

        foreach(var ui in building.NecessaryResource)
        {
            BuildingPriceUI priceUI = Instantiate(_priceUI);
            priceUI.UpdateUI(ui.NecessaryResource.resourceData.resourceIcon, ui.NecessaryResourceCount);
            priceUI.transform.SetParent(buildingPriceTrm.transform);
        }
    }

    public void OnPurchase()
    {
        _presenter.PurchaseBuilding(this);
    }

    public void UpdateUI()
    {
        lockedPanel.gameObject.SetActive(!building.IsUnlocked);
        buildingName.text = building.Name;
        buildingIcon.sprite = building.UISprite;
        maxInstallableCount.text = $"{building.CurrentInstallCount}/{building.MaxInstallableCount}";
    }

    public override void UIUpdate()
    {
        UpdateUI();
    }
}
