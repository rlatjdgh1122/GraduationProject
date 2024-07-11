using ArmySystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyConfigurer : BaseElementsConfigurer
{
    private string[] _enemyNames;
    private string[] _bossNames;

    private int curWave => WaveManager.Instance.CurrentWaveCount;

    private EnemyArmySpawnPatternsSO _enemyArmySpawnPatternsSO;

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames, EnemyArmySpawnPatternsSO enemyArmySpawnPatternsSO) : base(transform)
    {
        _enemyNames = enemyNames;

        _bossNames = bossNames;

        _enemyArmySpawnPatternsSO = enemyArmySpawnPatternsSO;
    }

    public List<Enemy> SetEnemy(List<Vector3> previousElementsPositions)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave && _bossNames != null)
        {
            //걍 하드코딩함
            Enemy spawnBoss;
            spawnBoss = PoolManager.Instance.Pop(_bossNames[(WaveManager.Instance.CurrentWaveCount / 5) - 1]) as Enemy;

            SetEnemyNav(spawnBoss);
            SetGroundElementsPosition(spawnBoss.gameObject, transform, previousElementsPositions);

            spawnedEnemies.Add(spawnBoss);
        }

        int randomIdx;

        if (curWave < 5) // 튜토리얼이면 자폭병 안 나오게
        {
            randomIdx = Random.Range(0, _enemyNames.Length - 1);
        }
        else
        {
            randomIdx = Random.Range(0, _enemyNames.Length);
        }

        int enemyCount = randomIdx == _enemyNames.Length ? 2 : GetRandomEnemyCount(); // 폭탄병이면 일단 2마리만 나오게. 아니면 So에서 설정한대로 랜덤

        for (int i = 0; i < enemyCount; i++)
        {
            string enemyName = _enemyNames[randomIdx];
            Enemy spawnEnemy = PoolManager.Instance.Pop(enemyName) as Enemy;

            SetEnemyNav(spawnEnemy);

            //SetGroundElementsPosition(spawnEnemy.gameObject, transform, previousElementsPositions);
            spawnedEnemies.Add(spawnEnemy);
        }

        EnemyArmyManager.Instance.CreateArmy(spawnedEnemies);
        SetEnemyPos(spawnedEnemies);
        return spawnedEnemies;
    }

    private void SetEnemyNav(Enemy spawnEnemy)
    {
        spawnEnemy.IsMove = false;
        spawnEnemy.NavAgent.enabled = false;
        spawnEnemy.ColliderCompo.enabled = false;
        spawnEnemy.MouseHandlerCompo.SetColiderActive(false);
    }

    private int GetRandomEnemyCount()
    {
        int waveCount = curWave;
        waveCount %= 6;
        int enemyCount = waveCount + 1;
        return enemyCount;
    }

    private void SetEnemyPos(List<Enemy> spawnEnemies)
    {
        EnemyArmySpawnPattern spawnPattern = GetEnemyArmySpawnPattern(spawnEnemies.Count);

        for (int i = 0; i < spawnEnemies.Count; i++)
        {
            GameObject spawnedEnemy = spawnEnemies[i].gameObject;

            spawnedEnemy.transform.SetParent(transform);

            spawnedEnemy.transform.localPosition = spawnPattern.EnemyArmySpawnPoints[i].position + new Vector3(0f, 1.85f, 0f);
            spawnedEnemy.transform.localScale = Vector3.one;
        }
    }

    private EnemyArmySpawnPattern GetEnemyArmySpawnPattern(int count)
    {
        return _enemyArmySpawnPatternsSO.EnemyArmySpawnPatterns[count - 2];
    }
}
