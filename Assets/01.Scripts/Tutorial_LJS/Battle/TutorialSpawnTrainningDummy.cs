using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSpawnTrainningDummy : MonoBehaviour
{
    [SerializeField] private EnemyBasicPenguin _trainningDummy;

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += FindEntity;
        SignalHub.OnGroundArrivedEvent += FindEntityAfterTime;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= FindEntity;
        SignalHub.OnGroundArrivedEvent -= FindEntityAfterTime;
    }

    private void FindEntityAfterTime()
    {
        FindRaft();
    }

    private void SpawnDummy(int count, Enemy[] enemies, Transform parent)
    {
        List<Enemy> trainingDummy = new();

        for(int i = 0; i <  count; i++)
        {
            var obj = Instantiate(_trainningDummy, enemies[i].transform.position, Quaternion.identity);
            obj.transform.SetParent(parent);

            trainingDummy.Add(obj);
        }

        EnemyArmyManager.Instance.CreateArmy(trainingDummy);
    }

    private void FindRaft()
    {
        Ground ground = transform.Find("Raft").GetComponentInChildren<Ground>();
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        ResourceObject[] resources = GetComponentsInChildren<ResourceObject>();

        foreach (var resource in resources)
        {
            resource.gameObject.SetActive(false);
        }

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        SpawnDummy(enemies.Length, enemies, ground.transform);
    }

    private void FindEntity()
    {
        Ground ground = GetComponentInChildren<Ground>();
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        ResourceObject[] resources = GetComponentsInChildren<ResourceObject>();

        foreach(var resource in resources)
        {
            resource.gameObject.SetActive(false);
        }

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        SpawnDummy(enemies.Length, enemies, ground.transform);
    }
}