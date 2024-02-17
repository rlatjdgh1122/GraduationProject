using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingItemInfo
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}
[CreateAssetMenu(menuName = "SO/BuildingList")]
public class BuildingDatabaseSO : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
