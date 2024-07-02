using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairPopup : BuildingUIComponent
{
    private Button _repairButton;

    public override void Awake()
    {
        base.Awake();

        _repairButton = transform.Find("Popup/InteractionButton").GetComponent<Button>();

        _repairButton.onClick.AddListener(OnRepair);
    }

    private void OnRepair()
    {
        ResourceManager.Instance.RemoveAllResources(15, () =>
        {
            buildingHealth.currentHealth = buildingHealth.maxHealth;
            HidePanel();
        });
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
