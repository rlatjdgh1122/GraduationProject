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
public struct BuffItemInfo
{
    [SerializeField]
    private float innerDistance;
    public float InnerDistance { get { return innerDistance; } }

    [SerializeField]
    private LayerMask targetLayer;
    public LayerMask TargetLayer { get { return targetLayer; } }

    [SerializeField]
    private float defaultBuffValue;
    public float DefaultBuffValue { get { return defaultBuffValue; } }
}

[System.Serializable]
public class BuildingItemInfo
{
    [SerializeField]
    private BuildingType buildingTypeEnum;
    public BuildingType BuildingTypeEnum { get { return buildingTypeEnum; } }

    [SerializeField]
    private string name;
    public string Name { get { return name; } }

    [SerializeField]
    private int id;
    public int ID { get { return id; } }

    [SerializeField]
    private Vector2Int size;
    public Vector2Int Size { get { return size; } }

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
    //public float NecessaryResources { get; private set; } //�� �ڿ� ������ �ڿ� ���� �ʿ�.

    [SerializeField]
    private int necessaryWokerCount;
    public int NecessaryWokerCount { get { return necessaryWokerCount; } }

    [SerializeField]
    private BuffItemInfo buffItemInfo;
    public BuffItemInfo BuffItemInfoST => buffItemInfo;
}

[CreateAssetMenu(menuName = "SO/BuildingList")]
public class BuildingDatabaseSO : ScriptableObject
{
   public List<BuildingItemInfo> BuildingItems = null;
}
