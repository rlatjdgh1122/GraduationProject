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
    ///  �� �� ��ŭ Ÿ��
    /// </summary>
    /// <param name="AfewTimes"> �� ��</param>
    public override void SpecialAttack(float AfewTimes)
    {
        DamageCasterCompo.CastSpecialDamage(AfewTimes);
    }
    public override void AoEAttack(float knbValue, float stunValue, float range = 0)
    {
        DamageCasterCompo.CaseAoEDamage(knbValue, stunValue);
    }

    public override void AoEPrickAttack(float knbValue, float stunValue, int damage)
    {
        DamageCasterCompo.CasePrickDamage(knbValue, stunValue, damage);
    }

    public override void MeleeAttack(float knbValue, float stunValue)
    {
        DamageCasterCompo.CastDamage(knbValue,stunValue);
    }

    public override void BombAttack()
    {
        DamageCasterCompo.CastBombDamage();
    }

    public override void DashAttack()
    {
        DamageCasterCompo.CastDashDamage();
    }
}
