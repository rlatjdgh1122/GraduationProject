using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/SynergyBuilding")]
public class SynergyBuildingStat : BaseStat
{
    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    public int GetUpgradedMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
}
