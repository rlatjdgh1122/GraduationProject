using ArmySystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyConfigurer : BaseElementsConfigurer
{
    private string[] _enemyNames;
    private string[] _bossNames;
    private string[] _generalNames;

    private int curWave => WaveManager.Instance.CurrentWaveCount;

    private EnemyArmySpawnPatternsSO _enemyArmySpawnPatternsSO;

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames, string[] generalNames, EnemyArmySpawnPatternsSO enemyArmySpawnPatternsSO) : base(transform)
    {
        _enemyNames = enemyNames;

        _bossNames = bossNames;

        _generalNames = generalNames;

        _enemyArmySpawnPatternsSO = enemyArmySpawnPatternsSO;
    }

    public List<Enemy> SetEnemy(List<Vector3> previousElementsPositions, bool isRaft)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();
        if (!isRaft)
        {
            if (isBossWave && _bossNames != null)
            {
                Enemy spawnBoss;
                spawnBoss = PoolManager.Instance.Pop(_bossNames[(WaveManager.Instance.CurrentWaveCount / 5) - 1]) as Enemy;

                SetEnemyNav(spawnBoss);
                SetGroundElementsPosition(spawnBoss.gameObject, transform, previousElementsPositions);

                spawnedEnemies.Add(spawnBoss);
            }

            if (isGeneralWave && _generalNames != null) // 장군
            {
                Enemy spawnGeneral;
                spawnGeneral = PoolManager.Instance.Pop(_generalNames[0]) as Enemy;

                SetEnemyNav(spawnGeneral);
                SetGroundElementsPosition(spawnGeneral.gameObject, transform, previousElementsPositions);

                spawnedEnemies.Add(spawnGeneral);
            }
        }

        int randomIdx = Random.Range(0, _enemyNames.Length);

        //if (curWave < 5) // 튜토리얼이면 자폭병 안 나오게
        //{
        //    randomIdx = Random.Range(0, _enemyNames.Length - 1);
        //}
        //else
        //{
            
        //}

        //int enemyCount = randomIdx == _enemyNames.Length ? 2 : GetRandomEnemyCount(); // 폭탄병이면 일단 2마리만 나오게. 아니면 So에서 설정한대로 랜덤
        int enemyCount = GetRandomEnemyCount(); // 폭탄병이면 일단 2마리만 나오게. 아니면 So에서 설정한대로 랜덤

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
        enemyCount = Mathf.Clamp(enemyCount, 2, 10);
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
