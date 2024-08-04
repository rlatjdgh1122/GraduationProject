using StatOperator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAttackableEntity : AnimalAttackableEntity
{
    public void StartEffect()
    {
    }

    public override void AoEAttack(float knbValue, float stunValue, float range = 0)
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
