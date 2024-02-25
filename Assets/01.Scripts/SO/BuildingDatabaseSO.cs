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
    [SerializeField]
    private BuildingType _buildingTypeEnum;
    public BuildingType BuildingTypeEnum { get { return _buildingTypeEnum; } }

    [SerializeField]
    private string name;
    public string Name { get { return name; } }

    [SerializeField]
    private int id;
    public int ID { get { return id; } }

    [SerializeField]
    private Vector2Int _size;
    public Vector2Int Size { get { return _size; } }

    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [SerializeField]
    private Sprite uiSprite;
    public Sprite UISprite { get { return uiSprite; } }

    [SerializeField]
    private float installedTime;
    public float InstalledTime { get { return installedTime; } }

    //[SerializeField]
    //public float NecessaryResources { get; private set; } //앙 자원 원석의 자원 설명 필요.

    [SerializeField]
    private int necessaryWokerCount;
    public int NecessaryWokerCount { get { return necessaryWokerCount; } }
}

[CreateAssetMenu(menuName = "SO/BuildingList")]
public class BuildingDatabaseSO : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
