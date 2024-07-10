using SynergySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeInfo
{
    public List<BuildingNecessaryResourceData> UpgradePriceList = new();

    [TextArea]
    public string BuildingUpgradeDescription;
    public Ability BuildingAbility;
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

    public BuildingNecessaryResourceData ReturnNeedResource(int index, ResourceType needResourceType)
    {
        return BuildingAbilityList[index].UpgradePriceList.Find(resource => resource.NecessaryResource.resourceData.resourceType == needResourceType);
    }
}