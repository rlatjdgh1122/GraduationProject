using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuildingDeadController : BuildingDeadController<BaseBuilding>
{
    private void OnEnable()
    {
        brokenBuilding.OnPushBuildingEvent += DestroyBuilding;
    }

    private void OnDisable()
    {
        brokenBuilding.OnPushBuildingEvent -= DestroyBuilding;
    }
}