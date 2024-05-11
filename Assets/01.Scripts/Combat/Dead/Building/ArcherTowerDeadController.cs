using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerDeadController : BuildingDeadController<ArcherTowerBuilding>
{
    public override void OnDied()
    {
        base.OnDied();

        Debug.Log("Broken ArcherTower");
    }

    public override void OnResurrected()
    {
        base.OnResurrected();
    }
}
