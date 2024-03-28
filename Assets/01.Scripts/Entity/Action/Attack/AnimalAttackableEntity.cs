using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AnimalAttack
{
    public HitType hitType;
    public int Damage;
    public float KnbackValue;
}

public class AnimalAttackableEntity : EntityAttackData
{
    public List<AnimalAttack> animalAttackList = new();

    public int ComboCounter { get; set; } //���� �޺� ��ġ
    public float LastAttackTime { get; set; } //���������� �����ߴ� �ð�
    [field: SerializeField] public float ComboWindow { get; private set; } //�ʱ�ȭ ��Ÿ��

    public override void AoEAttack(bool Knb, float value)
    {
        DamageCasterCompo.SelectTypeAOECast(
            animalAttackList[ComboCounter].Damage, 
            animalAttackList[ComboCounter].hitType,
            Knb,
            value);
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}