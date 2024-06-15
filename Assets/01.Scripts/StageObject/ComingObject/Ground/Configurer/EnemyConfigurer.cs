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

    public EnemyConfigurer(Transform transform, string[] enemyNames, string[] bossNames, ComingObjIncreaseRateDataSO comingObjIncreaseRateDataSO) : base(transform)
    {
        _enemyNames = enemyNames;

        _bossNames = bossNames;

        _comingObjIncreaseRateDataSO = comingObjIncreaseRateDataSO;
    }

    public List<Enemy> SetEnemy(List<Vector3> previousElementsPositions)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        if (isBossWave)
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

        if (WaveManager.Instance.CurrentWaveCount < 5) // Ʃ�丮���̸� ������ �� ������
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

            SetGroundElementsPosition(spawnEnemy.gameObject, transform, previousElementsPositions);
            spawnedEnemies.Add(spawnEnemy);
        }

        EnemyArmyManager.Instance.CreateArmy(spawnedEnemies);

        return spawnedEnemies;
    }

    private void SetEnemyNav(Enemy spawnEnemy)
    {
        spawnEnemy.IsMove = false;
        spawnEnemy.NavAgent.enabled = false;
        spawnEnemy.ColliderCompo.enabled = false;
    }

    protected override void SetGroundElementsPosition(GameObject spawnedElement, Transform transform, List<Vector3> previousElementsPositions)
    {
        // ���⼭ �������� ������ �� �ؾ���
        spawnedElement.transform.SetParent(transform);

        spawnedElement.transform.localPosition = new Vector3(0f, 1.85f, 0f);
        spawnedElement.transform.localScale = Vector3.one;
    }

    private int GetRandomEnemyCount()
    {
        int maxEnemyCount = 0;      

        if (curWave % 5 == 0) // ���� ���̺��
        {
            maxEnemyCount = Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.BossEnemyIncreaseRate);
        }
        else // �Ϲ� ���̺�
        {
            maxEnemyCount = Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.CommonEnemyIncreaseRate);
        }

        return Random.Range(1, maxEnemyCount);
    }
}
