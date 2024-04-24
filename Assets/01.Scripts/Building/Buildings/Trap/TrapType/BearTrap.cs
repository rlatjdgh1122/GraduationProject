using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BearTrap : BaseTrap
{
    [SerializeField]
    private float stunDuration; // ³ªÁß¿¡ ½ºÅÝ So·Î »©½Ã¿À
    private Animator _animator;

    public override void Init()
    {
        base.Init();
    }

    protected override void RemoveTrap()
    {
        _animator.SetBool("IsCatched", false);
        CoroutineUtil.CallWaitForOneFrame(base.RemoveTrap);
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    protected override void CatchEnemy(Enemy enemy, RaycastHit raycastHit)
    {
        Debug.Log("CatchEnemy");
        _damageCaster.CastBuildingStunDamage(enemy.HealthCompo,
                                             raycastHit,
                                             stunDuration,
                                             _characterStat.damage.GetValue());
        _animator.SetBool("IsCatched", true);
    }
}
