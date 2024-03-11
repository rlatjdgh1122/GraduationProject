using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeSkill : Skill
{
    [SerializeField] private float _targetValue;
    public float _currentValue = 0;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
        _owner.HealthCompo.OnHit += PlusGauge;
    }

    public void PlusGauge()
    {
        //if (!IsAvaliable) return;

        _currentValue++;

        if (_currentValue >= _targetValue)
        {
            OnSkillCompleted?.Invoke();
        }

        if (_currentValue >= 20)
        {
            OnSkillFailed?.Invoke();
        }
    }
}
