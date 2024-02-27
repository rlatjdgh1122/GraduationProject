using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackableEntity : EntityAttackData
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    ///  몇 배 만큼 타격
    /// </summary>
    /// <param name="AfewTimes"> 몇 배</param>
    public override void SpecialAttack(float AfewTimes)
    {
        DamageCasterCompo.CastSpecialDamage(AfewTimes);
    }

    public override void AoEAttack(bool Knb, float value)
    {
        DamageCasterCompo.CaseAoEDamage(Knb, value);
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}
