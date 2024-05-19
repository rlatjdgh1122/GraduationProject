using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDeadController : EntityDeadController<EnemyBear>
{
    public override void OnDied()
    {
        base.OnDied();

        _owner.DieEventHandler();

    }

    public override void OnResurrected()
    {
        base.OnResurrected();
        _owner.StateInit();

    }
}
