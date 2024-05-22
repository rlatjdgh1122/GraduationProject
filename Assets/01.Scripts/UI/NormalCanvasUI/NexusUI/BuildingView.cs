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

    public Button purchaseButton;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public Transform buildingPriceTrm;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    private BuildingItemInfo _building;
    public BuildingItemInfo Building => _building;

    protected override void Start()
    {
        _building = NexusManager.Instance.BuildingItemInfos[name];

        spawn = GetComponent<SpawnBuildingButton>();
        purchaseButton.onClick.AddListener(() => spawn.SpawnBuildingEventHandler(_building.Prefab.GetComponent<BaseBuilding>(), _building));

        UpdateUI();

        foreach(var ui in _building.NecessaryResource)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        Debug.Log($"{_building.CodeName}: {_building.IsUnlocked}");

        lockedPanel.gameObject.SetActive(!_building.IsUnlocked);
        buildingName.text = _building.Name;
        buildingIcon.sprite = _building.UISprite;
        maxInstallableCount.text = $"{_building.CurrentInstallCount}/{_building.MaxInstallableCount}";
    }

    public override void UIUpdate()
    {
        UpdateUI();
    }
}
