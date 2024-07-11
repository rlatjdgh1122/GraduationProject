using UnityEngine;
using UnityEngine.Events;

public class GaugeSkill : Skill
{
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
        if (HitValue > maxDecisionValue) return;

        HitValue++;
        OnHitValueChanged.Invoke(HitValue, maxDecisionValue);
    }

    public override void PlaySkill()
    {
        base.PlaySkill();

        HitValue = 0;
        OnHitValueChanged.Invoke(HitValue, maxDecisionValue);
    }
}
