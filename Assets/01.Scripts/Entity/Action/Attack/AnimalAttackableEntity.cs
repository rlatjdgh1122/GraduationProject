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

    public int ComboCounter { get; set; } //���� �޺� ��ġ
    public float LastAttackTime { get; set; } //���������� �����ߴ� �ð�
    [field: SerializeField] public float ComboWindow { get; private set; } //�ʱ�ȭ ��Ÿ��

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
