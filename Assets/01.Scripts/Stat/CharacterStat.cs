using UnityEngine;

public abstract class CharacterStat : BaseStat
{
    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (vitality.GetValue() * 5);
    }

    public override int GetDamage()
    {
        return damage.GetValue() + strength.GetValue();
    }
}
