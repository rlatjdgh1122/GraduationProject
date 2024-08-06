using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingRepairPopup : BuildingUIComponent
{
    [SerializeField] private NeedResource[] _needResources;
    private Button _repairButton;

    public UnityEvent OnRepairEvent = null;

    public override void Awake()
    {
        base.Awake();

        _repairButton = transform.Find("Popup/InteractionButton").GetComponent<Button>();

        _repairButton.onClick.AddListener(OnRepair);
    }

    private void OnRepair()
    {
        if (!ResourceManager.Instance.CheckAllResources(_needResources))
        {
            UIManager.Instance.ShowWarningUI("자원이 부족합니다!");
            return;
        }

        ResourceManager.Instance.RemoveResource(_needResources, () =>
        {
            synergyBuilding.DeadController.FixBuilding();
            synergyBuilding.Install(true);
            buildingHealth.ApplyHeal(1000);
            OnRepairEvent?.Invoke();
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
