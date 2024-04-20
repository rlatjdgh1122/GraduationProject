using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : BaseTrap
{
    [SerializeField]
    private float stunDuration; // ³ªÁß¿¡ ½ºÅÝ So·Î »©½Ã¿À
    Animator _animator;

    public override void Init()
    {
        base.Init();
        _animator.SetBool("IsCatched", false);
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    protected override void CatchEnemy(Enemy enemy, RaycastHit raycastHit)
    {
        _damageCaster.CastBuildingStunDamage(enemy.HealthCompo,
                                             raycastHit,
                                             stunDuration,
                                             _characterStat.damage.GetValue());
        _animator.SetBool("IsCatched", true);
    }
}
