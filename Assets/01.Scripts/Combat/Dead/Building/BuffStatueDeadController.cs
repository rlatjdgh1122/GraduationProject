using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStatueDeadController : BuildingDeadController<BuffBuilding>
{
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
