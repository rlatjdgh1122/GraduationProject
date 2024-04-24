using StatOperator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackableEntity : AnimalAttackableEntity
{

    [Header("Slash Effect")]
    [SerializeField] private ParticleSystem _leftHandEffectTransform;
    [SerializeField] private ParticleSystem _rightHandEffectTransform;

    public void StartEffect()
    {
        if (ComboCounter == 0)
        {
            _rightHandEffectTransform.Play();
        }
        else if (ComboCounter == 1)
        {
            _leftHandEffectTransform.Play();
        }
    }
    public override void AoEAttack(float knbValue, float stunValue)
    {
        // ���� ü���� ������ ���
        //ü�� ����ȭ
        //ü���� 
        //1 - ����ȭ��
        // �˹��� * (1 - ����ȭ��)

        float currentHealthPercentage = 1 - ((float)owner.HealthCompo.currentHealth / (float)owner.HealthCompo.maxHealth);

        float kncbackIncrease = animalAttackList[ComboCounter].KnbackValue * currentHealthPercentage;

        if (owner.HealthCompo.maxHealth / 2 >= owner.HealthCompo.currentHealth)
        {
            kncbackIncrease = animalAttackList[ComboCounter].KnbackValue;
        }

        base.AoEAttack(kncbackIncrease, stunValue);
    }
}
