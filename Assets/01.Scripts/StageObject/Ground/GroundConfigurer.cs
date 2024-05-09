using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum GroundElementsType
{
    Enemy,
    Resource,
    Reward
}

public class GroundConfigurer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject[] _resourcePrefabs;

    [Space]

    [SerializeField]
    private List<GameObject> _enemyPrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private List<GameObject> _bossPrefabs = new List<GameObject>();

    [Space]

    [SerializeField]
    private GameObject _normalRewardPrefab, _bossRewardPrefab;

    [Space]

    [SerializeField]
    private ResourceGeneratePatternDataSO _resourceGeneratePatternDataSO;

    private List<Vector3> _previousElementsPositions = new List<Vector3>();

    public GroundElements SetGroundElements(Transform groundTrm)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer(groundTrm,
                                                              _enemyPrefabs.Select(prefab => prefab.name).ToArray(),
                                                              _bossPrefabs.Select(prefab => prefab.name).ToArray());

        ResourceConfigurer resourceConfigurer = new ResourceConfigurer(groundTrm,
                                                                       _resourcePrefabs.Select(prefab => prefab.name).ToArray(),
                                                                       _resourceGeneratePatternDataSO.ResourceGeneratePatterns.ToArray());

        RewardConfigurer rewardConfigurer = new RewardConfigurer(groundTrm,
                                                                 _normalRewardPrefab.name,
                                                                 _bossRewardPrefab.name);

        return new GroundElements(resourceConfigurer.SetResource(_previousElementsPositions),
                                  rewardConfigurer.SetReward(_previousElementsPositions),
                                  enemyConfigurer.SetEnemy(_previousElementsPositions));
    }
}
