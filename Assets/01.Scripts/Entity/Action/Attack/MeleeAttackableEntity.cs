using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackableEntity : EntityAttackData
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void AoEAttack()
    {
        DamageCasterCompo.CaseAoEDamage();
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}
