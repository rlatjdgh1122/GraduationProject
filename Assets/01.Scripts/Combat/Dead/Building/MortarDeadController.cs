using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarDeadController : BuildingDeadController<MortarBuilding>
{
    public override void OnDied()
    {
        base.OnDied();

        //_owner.IsInstalled = false;
    }
}
