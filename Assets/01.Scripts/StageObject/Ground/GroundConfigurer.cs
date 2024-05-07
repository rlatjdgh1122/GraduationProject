using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundConfigurer : MonoBehaviour
{
    private readonly float D_setposY = 1.9f;
    private readonly float D_groundRadius = 5f;
    private readonly float D_checkDistance = 1.5f;

    [Header("Settings")]
    [SerializeField]
    private List<GameObject> _resourcePrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private List<GameObject> _enemyPrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private List<GameObject> _bossPrefabs = new List<GameObject>();
    private Queue<GameObject> _bossQueue = new Queue<GameObject>();

    [Space]

    [SerializeField]
    private GameObject _normalRewardPrefabs, _bossRewardPrefabs;

    private List<Vector3> _previousElementsPositions = new List<Vector3>();

    private bool isBossWave => WaveManager.Instance.CurrentWaveCount % 5 == 0; // 보스 나올 웨이브인지

    private void Start()
    {
        for (int i = 0; i < _bossPrefabs.Count; i++)
        {
            _bossQueue.Enqueue(_bossPrefabs[i]);
        }
    }

    public GroundElements SetGroundElements()
    {
        _previousElementsPositions.Clear();
        return new GroundElements(SetEnemy(), SetResource(), SetReward());
    }

    private ResourceObject[] SetResource()
    {
        float resourceCountProportion = 0.5f;
        int minResourceCount = 1;
        int maxResourceCount = 3;

        int resourceCount = GetRandomElementsCount(minResourceCount, maxResourceCount, resourceCountProportion);

        ResourceObject[] resources = new ResourceObject[resourceCount];

        for (int i = 0; i < resourceCount; i++)
        {
            int randomIdx = Random.Range(0, _resourcePrefabs.Count);
            string resourceName = _resourcePrefabs[randomIdx].name;
            ResourceObject spawnResource = PoolManager.Instance.Pop(resourceName) as ResourceObject;
            resources[i] = spawnResource;

            SetPosition(spawnResource.gameObject);
        }

        return resources;
    }

    private Enemy[] SetEnemy()
    {
        float enemyCountProportion = 0.5f;

        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave)
        {
            Enemy spawnBoss = PoolManager.Instance.Pop(_bossQueue.Dequeue().name) as Enemy;

            SetPosition(spawnBoss.gameObject);
            SetEnemyNav(spawnBoss);

            enemyCountProportion = 0.25f; // 보스 나오면 짜바리들은 좀 조금 나오게
            spawnedEnemies.Add(spawnBoss);
        }

        int minEnemyCount = 1;
        int maxEnemyCount = 5;

        int enemyCount = GetRandomElementsCount(minEnemyCount, maxEnemyCount, enemyCountProportion);

        for (int i = 0; i < enemyCount; i++)
        {
            int randomIdx = Random.Range(0, _enemyPrefabs.Count);
            GameObject enemy = _enemyPrefabs[randomIdx];
            Enemy spawnEnemy = PoolManager.Instance.Pop(enemy.name) as Enemy;

            SetEnemyNav(spawnEnemy);
            SetPosition(spawnEnemy.gameObject);

            spawnedEnemies.Add(spawnEnemy);
        }

        return spawnedEnemies.ToArray();
    }

    private CostBox SetReward()
    {
        if (isBossWave)
        {
            CostBox spawnReward = PoolManager.Instance.Pop(_bossRewardPrefabs.name) as CostBox;

            SetPosition(spawnReward.gameObject);

            return spawnReward;
        }

        if (Random.Range(0, 5) == 0)
        {
            CostBox spawnReward = PoolManager.Instance.Pop(_normalRewardPrefabs.name) as CostBox;

            SetPosition(spawnReward.gameObject);
            
            return spawnReward;
        }
        return null;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos;
        bool positionFound = false;

        do
        {
            randomPos = (Vector3)Random.insideUnitCircle * D_groundRadius;
            randomPos.y = D_setposY;

            positionFound = true;
            foreach (Vector3 prevPos in _previousElementsPositions)
            {
                if (Vector3.Distance(randomPos, prevPos) < D_checkDistance)
                {
                    positionFound = false;
                    break;
                }
            }

        } while (!positionFound);

        _previousElementsPositions.Add(randomPos);
        return randomPos;
    }

    private void SetPosition(GameObject spawnedElement)
    {
        spawnedElement.transform.SetParent(transform);

        Vector3 resourcePos = GetRandomPosition();

        spawnedElement.transform.localPosition = resourcePos;
        spawnedElement.transform.localScale = Vector3.one;
    }

    private void SetEnemyNav(Enemy spawnEnemy)
    {
        spawnEnemy.IsMove = false;
        spawnEnemy.NavAgent.enabled = false;
    }

    private int GetRandomElementsCount(int minCount, int maxCount, float countProportion)
    {
        int elementsCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * countProportion);
        elementsCount = Mathf.Clamp(elementsCount, minCount, maxCount);
        return elementsCount;
    }
}
