using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAttackableEntity : EntityAttackData
{
    [SerializeField] private int _attackEventValue;

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
            DamageCasterCompo.BleedCast(5, 3, 0.5f, HitType.BleedHit);
            Bleed = false;
        }
        else
        {
            DamageCasterCompo.CaseAoEDamage(false, 0);
        }
    }
}
