using UnityEngine;

public class DeadEnemy : DeadEntity<Enemy>
{
    public override void OnDied()
    {
        base.OnDied();
        _owner.DieEventHandler();
    }
}
