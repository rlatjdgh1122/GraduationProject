using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuildingDeadController : BuildingDeadController<SynergyBuilding>
{
    public override void OnDied()
    {
        base.OnDied();
    }

    public override void OnResurrected()
    {
        base.OnResurrected();
    }
}
