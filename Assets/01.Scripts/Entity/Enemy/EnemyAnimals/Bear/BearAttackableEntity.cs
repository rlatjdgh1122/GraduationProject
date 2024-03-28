using StatOperator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackableEntity : AnimalAttackableEntity
{
    private float _initialHealth;

    [Header("Slash Effect")]
    [SerializeField] private ParticleSystem _leftHandEffectTransform;
    [SerializeField] private ParticleSystem _rightHandEffectTransform;

    private float _currentKnockback = 0;

    protected override void Awake()
    {
        base.Awake();

        _initialHealth = owner.HealthCompo.maxHealth;
    }

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

    public override void AoEAttack(bool knockback, float value)
    {
        // ���� ü���� ������ ���
        //ü�� ����ȭ
        //ü���� 
        //1 - ����ȭ��
        // �˹��� * (1 - ����ȭ��)

        float currentHealthPercentage = 1 - ((float)owner.HealthCompo.currentHealth / (float)owner.HealthCompo.maxHealth);

        float kncbackIncrease = animalAttackList[ComboCounter].KnbackValue * currentHealthPercentage;

        if(owner.HealthCompo.maxHealth / 2 >= owner.HealthCompo.currentHealth)
        {
            kncbackIncrease = animalAttackList[ComboCounter].KnbackValue;
        }
        
        base.AoEAttack(animalAttackList[ComboCounter].KnbackValue > 0, kncbackIncrease);
    }
}