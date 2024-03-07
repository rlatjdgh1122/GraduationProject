using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : NexusPopupUI
{
    [HideInInspector]
    public BuildingItemInfo building;

    public TextMeshProUGUI buildingName;
    public Image buildingIcon;
    public TextMeshProUGUI buildingPrice;
    public TextMeshProUGUI maxInstallableCount;
    public Image lockedPanel;

    public override void Awake()
    {
        base.Awake();
    }

    public void UpdateDefaultUI()
    {
  
    }
}
