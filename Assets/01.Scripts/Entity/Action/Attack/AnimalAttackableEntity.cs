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
    public float StunValue;
    public float Range;
}

public class AnimalAttackableEntity : EntityAttackData
{
    public List<AnimalAttack> animalAttackList = new();

    public int ComboCounter { get; set; } //���� �޺� ��ġ
    public float LastAttackTime { get; set; } //���������� �����ߴ� �ð�
    [field: SerializeField] public float ComboWindow { get; private set; } //�ʱ�ȭ ��Ÿ��

    public override void AoEAttack(float knbValue, float stunValue,float range = 0)
    {
        var comboData = animalAttackList[ComboCounter];
        DamageCasterCompo.SelectTypeAOECast(
             comboData.Damage,
             comboData.hitType,
             comboData.SFXSound,
             knbValue,
             stunValue,
             comboData.Range);
    }

    public override void MeleeAttack(float knbValue, float stunValue)
    {
        DamageCasterCompo.CastDamage(knbValue, stunValue);
    }
}
