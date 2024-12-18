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
        SoundManager.Play3DSound(SoundName.SkillPolice, transform.position);
        HitValue = 0;
        OnHitValueChanged.Invoke(HitValue, maxDecisionValue);
    }
}
