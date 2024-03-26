using StatOperator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackableEntity : AnimalAttackableEntity
{
    private List<float> _savedKncbackValue;

    [Header("Percentage")]
    [Range(0,10)]   
    [SerializeField] private int _knockbackIncreaseToHPPercent;
    [Header("Slash Effect")]
    [SerializeField] private ParticleSystem _leftHandEffectTransform;
    [SerializeField] private ParticleSystem _rightHandEffectTransform;

    protected override void Awake()
    {
        base.Awake();

        foreach(var i in animalAttack)
        {
            _savedKncbackValue.Add(i.KnbackValue);
        }
    }

    public void StartEffect()
    {
        if(ComboCounter == 0)
        {
            _rightHandEffectTransform.Play();
        }
        if (ComboCounter == 1)
        {
            _leftHandEffectTransform.Play();
        }
    }

    public override void AoEAttack(bool Knb, float value)
    {
        float damageReceivedPercent = StatCalculator.ReturnPercent(owner.HealthCompo.maxHealth, owner.HealthCompo.currentHealth);

        if(damageReceivedPercent >= 50)
        {
            animalAttack[ComboCounter].KnbackValue = _savedKncbackValue[ComboCounter];
        }
        else
        {
            animalAttack[ComboCounter].KnbackValue *= damageReceivedPercent / 100;
        }
        

        base.AoEAttack(Knb, value);
    }
}