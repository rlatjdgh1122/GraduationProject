using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BearTrap : BaseTrap
{
    [SerializeField]
    private float stunDuration;
    private Animator _animator;

    public override void Init()
    {
        base.Init();
    }

    public override void RemoveTrap()
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
        _damageCaster.CastBuildingStunDamage(enemy.HealthCompo,
                                             raycastHit,
                                             stunDuration,
                                             _characterStat.damage.GetValue());
        _animator.SetBool("IsCatched", true);
        SoundManager.Play3DSound(SoundName.BearTrap, transform.position, 10f, 15f);
    }
}
