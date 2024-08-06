using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : NexusPopupUI
{
    [SerializeField]
    protected BuildingPriceUI _priceUI;

    [HideInInspector]
    public SpawnBuildingButton spawn;

    public Button purchaseButton;
    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public Transform buildingPriceTrm;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    protected BuildingItemInfo _building;
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

            var data = ResourceManager.Instance.TypeToResourceDataSO(ui.Type);

            priceUI.UpdateUI(data.resourceIcon, ui.Count);
            priceUI.transform.SetParent(buildingPriceTrm.transform);
        }
    }

    public virtual void OnPurchase()
    {
        _presenter.PurchaseBuilding(this);
    }

    public virtual void UpdateUI()
    {
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
