using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Defense,
    Buff,
    Resource
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
    private string _codeName;
    public string CodeName { get { return _codeName; } }

    [SerializeField]
    private int id;
    public int ID { get { return id; } }

    [SerializeField]
    private int _level = 1;
    public int Level => _level;

    [SerializeField]
    private int _price;
    public int Price => _price;

    [SerializeField]
    private string _description;
    public string Description => _description;

    [SerializeField]
    private Vector2Int _size;
    public Vector2Int Size { get { return _size; } }

    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } }

    [SerializeField]
    private Sprite _uiSprite;
    public Sprite UISprite { get { return _uiSprite; } }

    [SerializeField]
    private float installedTime;
    public float InstalledTime { get { return installedTime; } }

    [SerializeField]
    private int _currentInstallCount;
    public int CurrentInstallCount
    {
        get { return _currentInstallCount; }
        set { _currentInstallCount = value; }
    }

    [SerializeField]
    private int _maxInstallableCount;
    public int MaxInstallableCount { get { return _maxInstallableCount; } }

    [SerializeField]
    private Resource _necessaryResources; //이거 여러개로 나중에
    public Resource NecessaryResource { get { return _necessaryResources; } }

    [SerializeField]
    private int necessaryResourceCount;
    public int NecessaryResourceCount { get { return necessaryResourceCount; } }

    [SerializeField]
    private int necessaryWokerCount;
    public int NecessaryWokerCount { get { return necessaryWokerCount; } }

    public bool IsUnlocked = false;
}

[CreateAssetMenu(menuName = "SO/Building/BuildingList")]
public class BuildingDatabaseSO : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
