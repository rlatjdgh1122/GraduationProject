using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAttackableEntity : EntityAttackData
{
    [SerializeField] private ParticleSystem _bleedParticle;
    [SerializeField] private int _attackEventValue;
    [SerializeField] private int _bleedDmg;

    [SerializeField] private int _repeat;
    [SerializeField] private float _duration;

    public int AttackEventValue
    {
        get
        {
            return _attackEventValue;
        }
    }

    public bool Bleed;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void MeleeSphereAttack()
    {
        if(Bleed)
        {
            _bleedParticle.Play();
            DamageCasterCompo.BleedCast(_bleedDmg, _repeat, _duration, HitType.BleedHit);
            Bleed = false;
        }
        else
        {
            DamageCasterCompo.CaseAoEDamage(false, 0);
        }
    }
}
