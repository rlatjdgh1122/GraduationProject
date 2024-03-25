using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AnimalAttack
{
    public HitType hitType;
    public int Damage;
    public bool Knback;
    public float KnbackValue;
}

public class AnimalAttackableEntity : EntityAttackData
{
    public List<AnimalAttack> animalAttack = new();

    [field: SerializeField] public int ComboCounter { get; set; } //현재 콤보 수치
    [field: SerializeField] public float LastAttackTime { get; set; } //마지막으로 공격했던 시간
    [field: SerializeField] public float ComboWindow { get; private set; } //초기화 쿨타임

    public override void AoEAttack(bool Knb, float value)
    {
        DamageCasterCompo.SelectTypeAOECast(
            animalAttack[ComboCounter].Damage, 
            animalAttack[ComboCounter].hitType,
            animalAttack[ComboCounter].Knback,
            animalAttack[ComboCounter].KnbackValue);
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}
