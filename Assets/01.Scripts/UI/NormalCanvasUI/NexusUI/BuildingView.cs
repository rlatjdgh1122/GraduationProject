using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : NexusPopupUI
{
    [HideInInspector]
    public SpawnBuildingButton spawn;
    [HideInInspector]
    public BuildingItemInfo building;

    public Button purchaseButton;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public TextMeshProUGUI buildingPrice;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    public override void Awake()
    {
        base.Awake();

        OnUIUpdate += UpdateUI;
    }

    protected override void Start()
    {
        base.Start();
        building = _buildingDatabase.BuildingItems.FirstOrDefault(building => building.CodeName == this.name);
        spawn = GetComponent<SpawnBuildingButton>();
        SetDefaultUI();
    }

    public override void OnClick()
    {
        _presenter.PurchaseBuilding(this);
        base.OnClick();
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
            return;
        }
        else
        {
            lockedPanel.gameObject.SetActive(false);
            purchaseButton.onClick.AddListener(() => spawn.SpawnBuildingEventHandler(building.Prefab.GetComponent<BaseBuilding>(), building));
            UpdateUI();
        }
    }

    private void OnDisable()
    {
        OnUIUpdate -= UpdateUI;
    }
}
