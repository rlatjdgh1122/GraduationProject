using SynergySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeInfo
{
    public NeedResource[] UpgradePriceArr;

    [TextArea]
    public string BuildingUpgradeDescription;
    public Ability BuildingAbility;
    public int Reducehealingtime = 0;
}

[CreateAssetMenu(menuName = "SO/InfoData/Synergy")]
public class SynergyBuildingInfoDataSO : ScriptableObject
{
    [Header("Building Information")]
    public Sprite BuildingIcon;
    public string BuildingName;
    public SynergyType SynergyType;

    [Header("Synergy Abilties")]
    public List<UpgradeInfo> BuildingAbilityList = new();
}