using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public int GetCurrentPenguinCount()
    {
        Penguin[] penguins = FindObjectsOfType<Penguin>();

        int activeCount = 0;

        foreach (Penguin penguin in penguins)
        {
            if (penguin.enabled)
            {
                activeCount++;
            }
        }

        return activeCount;
    }

    private int currentPreparationTimeSpawnPenguinCount;
    public int CurrentPreparationTimeSpawnPenguinCount => currentPreparationTimeSpawnPenguinCount;

    public void PlusCurrentPreparationTimeSpawnPenguinCount()
    {
        currentPreparationTimeSpawnPenguinCount++;
    }

    public void ResetCurrentPreparationTimeSpawnPenguinCount()
    {
        currentPreparationTimeSpawnPenguinCount = 0;
    }

    public int GetDeadPenguinCount()
    {
        Penguin[] penguins = FindObjectsOfType<Penguin>();

        int count = 0;

        foreach (Penguin penguin in penguins)
        {
            if (penguin.IsDead)
                count++;
        }

        return count;
    }

    public int GetCurrentEnemyCount()
    {
        Enemy[] enemyPenguins = FindObjectsOfType<Enemy>();

        int activeCount = 0;

        foreach (Enemy enemyPenguin in enemyPenguins)
        {
            if (enemyPenguin.enabled)
            {
                activeCount++;
            }
        }

        return activeCount;
    }

    public int GetCurrentDeadEnemyCount()
    {
        Enemy[] enemyPenguins = FindObjectsOfType<Enemy>();

        int count = 0;

        foreach (Enemy enemyPenguin in enemyPenguins)
        {
            if (enemyPenguin.IsDead)
            {
                count++;
            }
        }

        return count;
    }

    public float ElapsedTime => Time.time;
    public Transform NexusTrm { get; private set; } = null;
    public Transform TentTrm { get; private set; } = null;
    public Transform WorkerSpawnPoint { get; private set; } = null;

    [SerializeField] private BuildingDatabaseSO buildingList = null;
    private Dictionary<string, BaseBuilding> _buildingDictionary = new();

    [SerializeField]
    private PoolingListSO _poolingListSO;

    public override void Awake()
    {
        TentTrm = FindObjectOfType<TentInitPos>().transform;
        NexusTrm = GameObject.Find("Nexus").transform;
        WorkerSpawnPoint = GameObject.Find("WorkerSpawnPoint").transform;
    }
    private void Start()
    {
        MakePool();
    }

    public BaseBuilding GetBuildingFormName(string buildingName)
    {
        if (_buildingDictionary.ContainsKey(buildingName))
        {
            Debug.Log($"{_buildingDictionary[buildingName]} 오브젝트가 성공적으로 소환됨");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("해당하는 이름의 건물은 존재하지 않습니다.");
        return null;
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingListSO.List.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount)); //리스트에 있는 모든
        _poolingListSO.DummyPenguinList.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
        _poolingListSO.GeneralPenguinList.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

}
