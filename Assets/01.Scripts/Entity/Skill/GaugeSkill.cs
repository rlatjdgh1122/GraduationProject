using UnityEngine;

public class GaugeSkill : Skill
{
    [SerializeField] private float _targetValue;  
    public float _hitValue = 0;

    private General General => _owner as General;

    bool IsUsed = false;

    public override void SetOwner(Entity owner)
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
                General.canSpinAttack = true;
                OnSkillCompleted?.Invoke();
                IsAvaliable = false;
            }
        }
    }
}
