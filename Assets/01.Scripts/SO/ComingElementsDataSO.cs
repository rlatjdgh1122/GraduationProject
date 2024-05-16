using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingElementsDataSO")]
public class ComingElementsDataSO : ScriptableObject
{
    [SerializeField]
    private List<GameObject> _enemiesList;
    public List<GameObject> EnemiesList => _enemiesList;

    [SerializeField]
    private List<GameObject> _bossList;
    public List<GameObject> BossList => _bossList;

    [SerializeField]
    private GameObject _normalRewardPrefab;
    public GameObject NormalRewardPrefab => _normalRewardPrefab;

    [SerializeField]
    private GameObject _bossRewardPrefab;
    public GameObject BossRewardPrefab => _bossRewardPrefab;

    [SerializeField]
    private ResourceGeneratePatternDataSO _resourceGeneratePatternDataSO;
    public ResourceGeneratePatternDataSO ResourceGeneratePatternDataSO => _resourceGeneratePatternDataSO;

    [SerializeField]
    private GameObject _magicCagePrefab;
    public GameObject MagicCagePrefab => _magicCagePrefab;
}
