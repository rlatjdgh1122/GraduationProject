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

    public override void AoEAttack(bool Knb, float value)
    {
        DamageCasterCompo.CaseAoEDamage(Knb, value);
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}