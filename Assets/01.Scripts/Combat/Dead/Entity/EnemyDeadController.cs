using UnityEngine;

public class EnemyDeadController : EntityDeadController<Enemy>
{

    public override void OnDied()
    {
        base.OnDied();

        _owner.MyArmy.RemoveEnemy(_owner);
        _owner.DieEventHandler();

        _owner.MouseHandlerCompo.SetColiderActive(false);
    }

    public override void OnResurrected()
    {
        //체력 다시 채움
        _owner.HealthCompo.SetHealth(_owner.Stat);

        _owner.MouseHandlerCompo.SetColiderActive(true);

        _anim.speed = 1f;
        //애니메이션 켜줌
        _anim.enabled = true;
        //죽는 애니메이션 꺼줌
        _anim.SetBool(HASH_DEAD, false);
        //엔티티 살려줌
        _owner.IsDead = false;
        //엔티티 스크립트 켜줌
        _owner.enabled = true;
    }
}
