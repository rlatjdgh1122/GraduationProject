using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    DefenseBuilding,
    BuffBuilding,
    ResourceBuilding
}

public enum DefaultBuildingType
{
    ConstructionStation,
    PenguinSpawn
}

[System.Serializable]
public class BuildingItemInfo
{
    [field: SerializeField]
    public BuildingType BuildingType { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public Sprite UISprite { get; private set; }


    [field: SerializeField]
    public float installedTime { get; private set; }
    //[field: SerializeField]
    //public float NecessaryResources { get; private set; } //�� �ڿ� ������ �ڿ� ���� �ʿ�.
    [field: SerializeField]
    public int NecessaryWokerCount { get; private set; }
}
[CreateAssetMenu(menuName = "SO/BuildingList")]
public class BuildingDatabaseSO : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
