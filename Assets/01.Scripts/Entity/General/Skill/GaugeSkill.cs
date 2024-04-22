using UnityEngine;

public class GaugeSkill : Skill
{
    [SerializeField] private float _targetValue;
    public float _hitValue = 0;

    bool IsUsed = false;

    public override void SetOwner(General owner)
    {
        base.SetOwner(owner);

        IsUsed = false;
        _owner.HealthCompo.OnHit += PlusGauge;
    }

    public void PlusGauge()
    {
        if (!IsAvaliable) return;

        if (_owner.HealthCompo.currentHealth <= _owner.HealthCompo.maxHealth * 0.5f)
        {
            if (!IsUsed)
            {
                OnSkillStart?.Invoke();
                IsUsed = true;
            }

            _hitValue++;

            if (_hitValue >= _targetValue)
            {
                _owner.canSpinAttack = true;
                OnSkillCompleted?.Invoke();
                IsAvaliable = false;
            }
        }
    }
}
