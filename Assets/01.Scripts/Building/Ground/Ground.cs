using System.Collections.Generic;
using UnityEngine;
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

    [Space]

    [SerializeField]
    private GameObject _rewardPrefabs;


    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo => _outline;

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
        int minResourceCount = 1;
        int maxResourceCount = 3;

        int resourceCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * resourceCountProportion);
        resourceCount = Mathf.Clamp(resourceCount, minResourceCount, maxResourceCount);

        for (int i = 0; i < resourceCount; i++)
        {
            int randomIdx = Random.Range(0, _resourcePrefabs.Count);
            GameObject resourceName = _resourcePrefabs[randomIdx];
            GameObject spawnResource = PoolManager.Instance.Pop(resourceName.name).gameObject;
            spawnResource.transform.SetParent(transform);

            Vector3 resourcePos = GetRandomPosition();

            spawnResource.transform.localPosition = resourcePos;
            //spawnResource.transform.localScale = Vector3.one;
        }
    }

    private void SetEnemy()
    {
        float enemyCountProportion = 0.5f;

        List<Enemy> spawnedEnemies = new();

        if (WaveManager.Instance.CurrentWaveCount == 5) // 일단 보스
        {
            Enemy spawnBoss = PoolManager.Instance.Pop(_bossPrefabs[0].name) as Enemy;
            spawnBoss.transform.SetParent(transform);

            Vector3 enemyPos = GetRandomPosition();
            spawnBoss.transform.localPosition = enemyPos;

            spawnBoss.IsMove = false;
            spawnBoss.NavAgent.enabled = false;

            spawnBoss.transform.localScale = Vector3.one; // 고릴라는 0.7임

            enemyCountProportion = 0.25f;
            spawnedEnemies.Add(spawnBoss);
        }

        int minEnemyCount = 1;
        int maxEnemyCount = 5;

        int enemyCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * enemyCountProportion);
        enemyCount = Mathf.Clamp(enemyCount, minEnemyCount, maxEnemyCount);
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIdx = Random.Range(0, _enemyPrefabs.Count);
            GameObject enemy = _enemyPrefabs[randomIdx];
            Enemy spawnEnemy = PoolManager.Instance.Pop(enemy.name) as Enemy;
            spawnEnemy.transform.SetParent(transform);

            Vector3 enemyPos = GetRandomPosition();
            spawnEnemy.transform.localPosition = enemyPos;

            spawnEnemy.IsMove = false;
            spawnEnemy.NavAgent.enabled = false;

            spawnedEnemies.Add(spawnEnemy);
        }

        _groundMove.SetEnemies(spawnedEnemies.ToArray());
    }

    private void SetReward()
    {
        if (Random.Range(0, 5) == 0)
        {
            // 무언가가 발생한 경우, 원하는 작업을 수행
            GameObject spawnReward = PoolManager.Instance.Pop(_rewardPrefabs.name).gameObject;
            spawnReward.transform.SetParent(transform);

            Vector3 rewardPos = GetRandomPosition();

            spawnReward.transform.localPosition = rewardPos;
        }
    }

    private List<Vector3> previousRewardPositions = new List<Vector3>();

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = (Vector3)Random.insideUnitCircle * D_groundRadius;
        randomPos.y = D_setposY;

        foreach (Vector3 prevPos in previousRewardPositions)
        {
            if (Vector3.Distance(randomPos, prevPos) < D_checkDistance)
            {
                return GetRandomPosition();
            }
        }

        previousRewardPositions.Add(randomPos);
        return randomPos;
    }

    public void SetMoveTarget(Transform trm)
    {
        _groundMove.SetMoveTarget(trm);
    }
}
