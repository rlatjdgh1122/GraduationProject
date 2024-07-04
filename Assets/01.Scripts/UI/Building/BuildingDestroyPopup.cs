using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyPopup : BuildingUIComponent
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        synergyBuilding.DeadController.DestroyBuilding();

        UIManager.Instance.HidePanel("BuildingUI");
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
