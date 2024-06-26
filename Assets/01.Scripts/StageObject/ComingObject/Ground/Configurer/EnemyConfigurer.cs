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

    private ComingObjIncreaseRateDataSO _comingObjIncreaseRateDataSO;
    private EnemyArmySpawnPatternsSO _enemyArmySpawnPatternsSO;

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames, ComingObjIncreaseRateDataSO comingObjIncreaseRateDataSO, EnemyArmySpawnPatternsSO enemyArmySpawnPatternsSO) : base(transform)
    {
        _enemyNames = enemyNames;

        _bossNames = bossNames;

        _comingObjIncreaseRateDataSO = comingObjIncreaseRateDataSO;
        _enemyArmySpawnPatternsSO = enemyArmySpawnPatternsSO;
    }

    public List<Enemy> SetEnemy(List<Vector3> previousElementsPositions)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave && _bossNames != null)
        {
            //�� �ϵ��ڵ���
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

            spawnedEnemies.Add(spawnBoss);
        }

        int randomIdx;

        if (curWave < 5) // Ʃ�丮���̸� ������ �� ������
        {
            randomIdx = Random.Range(0, _enemyNames.Length - 1);
        }
        else
        {
            randomIdx = Random.Range(0, _enemyNames.Length);
        }

        int enemyCount = randomIdx == _enemyNames.Length ? 2 : GetRandomEnemyCount(); // ��ź���̸� �ϴ� 2������ ������. �ƴϸ� So���� �����Ѵ�� ����

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
    }

    private int GetRandomEnemyCount()
    {
        int maxEnemyCount = 0;
        int minEnemyCount = (int)(curWave * 0.335f); // 3�����븶�� �ּ� �����Ǵ� ����� ���� 1�� �þ

        if(minEnemyCount < 2) { minEnemyCount = 2; } //�ּ� 2������ ������ ��

        if (curWave % 5 == 0) // ���� ���̺��
        {
            maxEnemyCount = Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.BossEnemyIncreaseRate);
        }
        else // �Ϲ� ���̺�
        {
            maxEnemyCount = Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.CommonEnemyIncreaseRate);
        }

        return Random.Range(minEnemyCount, maxEnemyCount); 
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
