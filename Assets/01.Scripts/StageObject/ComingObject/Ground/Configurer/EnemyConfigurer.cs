using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyConfigurer : BaseElementsConfigurer
{
    private string[] _enemyNames;
    private string[] _bossNames;

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames) : base(transform)
    {
        _enemyNames = enemyNames;

        _bossNames = bossNames;
    }

    public Enemy[] SetEnemy(List<Vector3> previousElementsPositions, bool isRaft)
    {
        float enemyCountProportion = 0.5f;

        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave)
        {
            //걍 하드코딩함
            Enemy spawnBoss;
            if (WaveManager.Instance.CurrentWaveCount == 5)
            {
                spawnBoss = PoolManager.Instance.Pop(_bossNames[0]) as Enemy;
            }
            else
            {
                spawnBoss = PoolManager.Instance.Pop(_bossNames[1]) as Enemy;
            }

            SetEnemyNav(spawnBoss);
            SetGroundElementsPosition(spawnBoss.gameObject, transform, previousElementsPositions);

            enemyCountProportion = 0.25f; // 보스 나오면 짜바리들은 조금만 나오게
            spawnedEnemies.Add(spawnBoss);
        }

        int minEnemyCount = 1;
        int maxEnemyCount = 10;

        if(isRaft) { maxEnemyCount = 1; }

        int enemyCount = GetRandomElementsCount(minEnemyCount, maxEnemyCount, enemyCountProportion);

        for (int i = 0; i < enemyCount; i++)
        {
            int randomIdx;
            
            if (WaveManager.Instance.CurrentWaveCount < 5) // 튜토리얼이면 자폭병 안 나오게
            {
                randomIdx = Random.Range(0, _enemyNames.Length - 1);
            }
            else
            {
                randomIdx = Random.Range(0, _enemyNames.Length);
            }
            
            string enemyName = _enemyNames[randomIdx];
            Enemy spawnEnemy = PoolManager.Instance.Pop(enemyName) as Enemy;

            Debug.Log($"{isRaft} : {spawnEnemy.GetInstanceID()}");

            SetEnemyNav(spawnEnemy);

            if (isRaft)
            {
                SetRaftElementsPosition(spawnEnemy.gameObject, transform);
                spawnEnemy.transform.rotation = Quaternion.identity;
                spawnEnemy.transform.position += new Vector3(0, 1f, 0f);
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
        spawnEnemy.ColliderCompo.enabled = false;
    }
}
