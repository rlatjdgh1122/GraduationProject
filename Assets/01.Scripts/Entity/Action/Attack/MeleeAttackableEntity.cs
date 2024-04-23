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

    public override void AoEAttack(float knbValue)
    {
        DamageCasterCompo.CaseAoEDamage(knbValue);
    }
    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }

    public override void DashAttack()
    {
        DamageCasterCompo.CastDashDamage();
    }
}
