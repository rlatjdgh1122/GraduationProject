using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfigurer : BaseElementsConfigurer
{
    private string[] _enemyNames;

    private Queue<string> _bossQueue = new Queue<string>();

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames) : base(transform)
    {
        _enemyNames = enemyNames;

        foreach (var bossName in bossNames)
        {
            _bossQueue.Enqueue(bossName);
        }
    }

    public Enemy[] SetEnemy(List<Vector3> previousElementsPositions, bool isRaft)
    {
        float enemyCountProportion = 0.5f;

        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave)
        {
            Enemy spawnBoss = PoolManager.Instance.Pop(_bossQueue.Dequeue()) as Enemy;

            SetEnemyNav(spawnBoss);

            enemyCountProportion = 0.25f; // 보스 나오면 짜바리들은 조금만 나오게
            spawnedEnemies.Add(spawnBoss);
        }

        int minEnemyCount = 1;
        int maxEnemyCount = 5;

        int enemyCount = GetRandomElementsCount(minEnemyCount, maxEnemyCount, enemyCountProportion);

        for (int i = 0; i < enemyCount; i++)
        {
            int randomIdx = Random.Range(0, _enemyNames.Length);
            string enemyName = _enemyNames[randomIdx];
            Enemy spawnEnemy = PoolManager.Instance.Pop(enemyName) as Enemy;

            SetEnemyNav(spawnEnemy);

            if (isRaft)
            {
                SetRaftElementsPosition(spawnEnemy.gameObject, transform);
            }
            else
            {
                SetGroundElementsPosition(spawnEnemy.gameObject, transform, previousElementsPositions);
            }

            spawnedEnemies.Add(spawnEnemy);
        }

        return spawnedEnemies.ToArray();
    }

    private void SetEnemyNav(Enemy spawnEnemy)
    {
        spawnEnemy.IsMove = false;
        spawnEnemy.NavAgent.enabled = false;
    }
}
