using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public int GetPenguinCount => FindObjectsOfType<Penguin>().Length;

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

    public int GetEnemyPenguinCount()
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

    public int GetDeadEnemyPenguinCount()
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
    private int dummyPenguinCount;
    public int GetDummyPenguinCount => dummyPenguinCount;

    [SerializeField] private InitBuildingList buildingList = null;
    private Dictionary<string, Building> _buildingDictionary = new();

    [Header("스크립트들")]
    [SerializeField] private MainUI mainUI = null;

    [SerializeField]
    private PoolingListSO _poolingListSO;

    //private void Start()
    //{
    //    buildingList.BuildingItems.ForEach(item =>
    //    {
    //        mainUI.SetBuildingItemUI(item.Name, item.Image);
    //        _buildingDictionary.Add(item.Name, item.BuildItem);
    //    });
    //    Init();
    //}

    private void Start()
    {
        MakePool();
    }

    public Building GetBuildingFormName(string buildingName)
    {
        if (_buildingDictionary.ContainsKey(buildingName))
        {
            Debug.Log($"{_buildingDictionary[buildingName]} 오브젝트가 성공적으로 소환됨");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("해당하는 이름의 건물은 존재하지 않습니다.");
        return null;
    }

    public Ray RayPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        return ray;
    }

    public bool TryRaycast(Ray ray, out RaycastHit hit,float distance, LayerMask? layerMask = null) // PenguinSpawner Update에 사용하는거 나와있음
    {
        return Physics.Raycast(ray, out hit, distance, layerMask ?? Physics.DefaultRaycastLayers);
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingListSO.List.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount)); //리스트에 있는 모든
    }

    public void PlusDummyPenguinCount()
    {
        dummyPenguinCount++;
    }

    public void ResetDummyPenguinCount()
    {
        dummyPenguinCount = 0;
    }
}
