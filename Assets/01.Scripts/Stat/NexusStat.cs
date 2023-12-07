using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Nexus")]
public class NexusStat : BaseStat
{
    public virtual void SetOwner(NexusBase owner)
    {
        _nexus = owner;
    }

    public override int GetDamage()
    {
        return 0;
    }

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (armor.GetValue() * 5);
    }
}
