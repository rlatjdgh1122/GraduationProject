using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildItemInfo
{
    public string Name;
    public Sprite Image;
    public Build BuildItem;
}
[CreateAssetMenu(menuName = "SO/BuildingList")]
public class InitBuildingList : ScriptableObject
{
   public List<BuildItemInfo> BuildItems = null;
}
