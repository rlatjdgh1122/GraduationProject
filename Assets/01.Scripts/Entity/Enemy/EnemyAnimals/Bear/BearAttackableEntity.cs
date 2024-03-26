using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackableEntity : AnimalAttackableEntity
{
    [SerializeField] private ParticleSystem _leftHandEffectTransform;
    [SerializeField] private ParticleSystem _rightHandEffectTransform;

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
}
