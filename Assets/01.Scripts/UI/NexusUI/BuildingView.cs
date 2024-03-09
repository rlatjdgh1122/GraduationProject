using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : NexusPopupUI
{
    [HideInInspector]
    public BuildingItemInfo building;
    [HideInInspector]
    public SpawnBuildingButton spawn;

    public Button purchaseButton;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public TextMeshProUGUI buildingPrice;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    public override void Awake()
    {
        base.Awake();

        spawn = GetComponent<SpawnBuildingButton>();
        SetDefaultUI();
    }

    public void OnPurchase()
    {
        presenter.PurchaseBuilding(this);

        UpdateUI();
    }

    public void UpdateUI()
    {
        buildingName.text = building.Name;
        buildingIcon.sprite = building.UISprite;
        buildingPrice.text = $"{building.Price}";
        maxInstallableCount.text = $"{building.CurrentInstallCount}/{building.MaxInstallableCount}";
    }

    public void SetDefaultUI()
    {
        if (!building.IsUnlocked)
        {
            UpdateUI();
            return;
        }
        else
        {
            lockedPanel.gameObject.SetActive(false);
            purchaseButton.onClick.AddListener(() => spawn.SpawnBuildingEventHandler(building.Prefab.GetComponent<BaseBuilding>(), building));
            UpdateUI();
        }
    }
}
