using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public enum OutlineColorType
{
    Green,
    Red,
    None
}

[RequireComponent(typeof(Outline))]
public class Ground : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private List<GameObject> _resourcePrefabs = new List<GameObject>();
    
    [Space]

    [SerializeField]
    private List<GameObject> _enemyPrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private List<GameObject> _bossPrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private GameObject _rewardPrefabs;



    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo =>_outline;

    private GroundMove _groundMove;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _groundMove = GetComponent<GroundMove>();
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;
        UpdateOutlineColor(OutlineColorType.None);
    }

    public void UpdateOutlineColor(OutlineColorType type)
    {
        _outline.enabled = true;
        _outline.OutlineWidth = 2.0f;
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        switch (type)
        {
            case OutlineColorType.Green:
                _outline.OutlineColor = Color.green;
                break;
            case OutlineColorType.Red:
                _outline.OutlineColor = Color.red;
                break;
            case OutlineColorType.None:
                _outline.enabled = false;
                break;
        }
    }

    public void SetGroundInfo(Transform parentTransform, Vector3 position)
    {
        SetEnemy();
        SetReward();
        SetResource();
        _groundMove.SetGroundInfo(parentTransform,
                                  position);
    }

    private void SetResource()
    {
        float resourceCountProportion = 0.5f;
        int minResourceCount = 0;
        int maxResourceCount = 3;

        int resourceCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * resourceCountProportion);
        resourceCount = Mathf.Clamp(resourceCount, minResourceCount, maxResourceCount);

        for (int i = 0; i < resourceCount; i++)
        {
            int randomIdx = Random.Range(0, _resourcePrefabs.Count);
            GameObject resource = _resourcePrefabs[randomIdx];
            PoolManager.Instance.Pop(resource.name);
            Debug.Log($"{resource}자원 생성");
        }
    }

    private void SetEnemy()
    {
        float enemyCountProportion = 0.5f;
        int minEnemyCount = 1;
        int maxEnemyCount = 5;

        int enemyCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * enemyCountProportion);
        enemyCount = Mathf.Clamp(enemyCount, minEnemyCount, maxEnemyCount);

        for(int i = 0; i < enemyCount; i++)
        {
            int randomIdx = Random.Range(0, _enemyPrefabs.Count);
            GameObject enemy = _enemyPrefabs[randomIdx];
            PoolManager.Instance.Pop(enemy.name);
            Debug.Log($"{enemy}적 생성");
        }

    }

    private void SetReward()
    {
        if (Random.Range(0, 5) == 0)
        {
            // 무언가가 발생한 경우, 원하는 작업을 수행
            PoolManager.Instance.Pop(_rewardPrefabs.name);
            Debug.Log("거시기 보상박스 생성");
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _groundMove.SetMoveTarget(trm);
    }
}
