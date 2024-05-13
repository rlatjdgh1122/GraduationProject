using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignaTowerDeadController : BuildingDeadController<SignalTower>
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