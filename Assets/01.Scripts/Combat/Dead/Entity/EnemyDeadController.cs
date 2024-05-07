using UnityEngine;

public class EnemyDeadController : EntityDeadController<Enemy>
{
    public override void OnDied()
    {
        base.OnDied();

        _owner.DieEventHandler();
    }

    public override void OnResurrected()
    {
        base.OnResurrected();

    }
}
