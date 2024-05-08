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
    private GameObject _normalRewardPrefabs, _bossRewardPrefabs;

    private Vector3 _groundPos;

    private List<Vector3> _previousElementsPositions = new List<Vector3>();
    private Dictionary<GroundElementsType, BaseConfigurer> _elementsConfigurers = new Dictionary<GroundElementsType, BaseConfigurer>();

    private void Awake()
    {
        _elementsConfigurers.Add(GroundElementsType.Enemy,
                                new EnemyConfigurer(transform,
                                                    _enemyPrefabs.Select(prefab => prefab.name).ToArray(),
                                                    _bossPrefabs.Select(prefab => prefab.name).ToArray()));

        _elementsConfigurers.Add(GroundElementsType.Resource,
                                new ResourceConfigurer(transform,
                                                       _resourcePrefabs.Select(prefab => prefab.name).ToArray()));

        _elementsConfigurers.Add(GroundElementsType.Reward,
                                new RewardConfigurer(transform,
                                                    _normalRewardPrefabs.name,
                                                    _bossRewardPrefabs.name));
    }

    public GroundElements SetGroundElements(Transform groundTrm)
    {
        _previousElementsPositions.Clear();
        transform.SetParent(groundTrm);
        transform.localPosition = Vector3.zero;

        EnemyConfigurer enemyConfigurer = _elementsConfigurers[GroundElementsType.Enemy] as EnemyConfigurer;
        ResourceConfigurer resourceConfigurer = _elementsConfigurers[GroundElementsType.Resource] as ResourceConfigurer;
        RewardConfigurer rewardConfigurer = _elementsConfigurers[GroundElementsType.Reward] as RewardConfigurer;

        return new GroundElements(enemyConfigurer.SetEnemy(_previousElementsPositions),
                                  resourceConfigurer.SetResource(_previousElementsPositions),
                                  rewardConfigurer.SetReward(_previousElementsPositions));
    }
}
