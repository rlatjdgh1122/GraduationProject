using UnityEngine;
using UnityEngine.Events;

public class GaugeSkill : Skill
{
    [SerializeField] private float _targetValue;  
    public float HitValue = 0;

    public UnityEvent<float, float> OnHitValueChanged;

    public ShieldGeneralPenguin shieldPenguin => _owner as ShieldGeneralPenguin;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);

        _owner.HealthCompo.OnHit += PlusGauge;
    }

    public void PlusGauge()
    {
        if (!IsAvaliable) return;

        HitValue++;

        if (HitValue >= _targetValue)
        {
            shieldPenguin.OnPassiveHitEvent();
            HitValue = 0;
            OnHitValueChanged?.Invoke(HitValue, _targetValue);
        }

        OnHitValueChanged?.Invoke(HitValue, _targetValue);
    }
}
