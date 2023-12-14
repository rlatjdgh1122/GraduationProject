using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingItemInfo
{
    public string Name;
    public Sprite Image;
    public BaseBuilding BuildItem;
}
[CreateAssetMenu(menuName = "SO/BuildingList")]
public class InitBuildingList : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
