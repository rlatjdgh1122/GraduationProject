using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSpawnTrainningDummy : MonoBehaviour
{
    [SerializeField] private EnemyBasicPenguin _trainningDummy;

    [SerializeField] private TutorialWorldCanvas _tutorialCanvas;
    private Transform _trainingDummyTrm;

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += FindEntity;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= FindEntity;
    }

    public void ShowArrow()
    {
        _tutorialCanvas.Init(TutorialImage.Arrow_Left);
        _tutorialCanvas.SetTarget(_trainingDummyTrm, 5f);
        _tutorialCanvas.ShowUI();
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

        _trainingDummyTrm = trainingDummy[0].transform;
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