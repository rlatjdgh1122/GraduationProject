using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Nexus")]
public class NexusStat : BaseStat
{
    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (armor.GetValue() * 5);
    }
}
