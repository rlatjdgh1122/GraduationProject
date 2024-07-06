using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStatueDeadController : BuildingDeadController<BuffBuilding>
{
    private void OnEnable()
    {
        brokenBuilding.OnPushBuildingEvent += DestroyBuilding;
    }

    private void OnDisable()
    {
        brokenBuilding.OnPushBuildingEvent -= DestroyBuilding;
    }

    public override void OnDied()
    {
        base.OnDied();

        _owner.StopEffect();
    }

    public override void OnResurrected()
    {
        base.OnResurrected();
    }
}
