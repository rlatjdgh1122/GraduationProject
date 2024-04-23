using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AnimalAttack
{
    public HitType hitType;
    public SoundName SFXSound;
    public int Damage;
    public float KnbackValue;
}

public class AnimalAttackableEntity : EntityAttackData
{
    public List<AnimalAttack> animalAttackList = new();

    public int ComboCounter { get; set; } //현재 콤보 수치
    public float LastAttackTime { get; set; } //마지막으로 공격했던 시간
    [field: SerializeField] public float ComboWindow { get; private set; } //초기화 쿨타임

    public override void AoEAttack(float knbValue = 0)
    {
        DamageCasterCompo.SelectTypeAOECast(
            animalAttackList[ComboCounter].Damage, 
            animalAttackList[ComboCounter].hitType,
            animalAttackList[ComboCounter].SFXSound,
            knbValue);
    }

    public override void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }
}
