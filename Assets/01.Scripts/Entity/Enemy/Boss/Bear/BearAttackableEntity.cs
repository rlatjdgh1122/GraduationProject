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
        // 현재 체력을 비율로 계산
        //체력 정규화
        //체력이 
        //1 - 정규화값
        // 넉백힘 * (1 - 정규화값)

        float currentHealthPercentage = 1 - ((float)owner.HealthCompo.currentHealth / (float)owner.HealthCompo.maxHealth);

        float kncbackIncrease = animalAttackList[ComboCounter].KnbackValue * currentHealthPercentage;

        if (owner.HealthCompo.maxHealth / 2 >= owner.HealthCompo.currentHealth)
        {
            kncbackIncrease = animalAttackList[ComboCounter].KnbackValue;
        }

        base.AoEAttack(kncbackIncrease, stunValue);
    }
}
