using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Nexus")]
public class NexusStat : BaseStat
{
    [Header("Level")]
    public int level;
    public int upgradePrice;
    public int needResourceHigh;
    public int needResourceLow;
    public int priceIncreaseValue;
    public int levelupIncreaseValue;

    [HideInInspector]
    public BuildingItemInfo unlockedBuilding;
    [HideInInspector]
    public BuildingItemInfo previewBuilding;

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    public int GetUpgradedMaxHealthValue()
    {
        return maxHealth.GetValue() + (level * levelupIncreaseValue);
    }
}
