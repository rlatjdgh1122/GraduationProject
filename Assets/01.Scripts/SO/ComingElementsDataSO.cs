using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingElementsDataSO")]
public class ComingElementsDataSO : ScriptableObject
{
    private Dictionary<int, List<GameObject>> _enmies;
    public Dictionary<int, List<GameObject>> Enmies
    {
        get
        {
            if (_enmies == null)
            {
                _enmies = new Dictionary<int, List<GameObject>>
                {
                    { 0, _normalEnemiesList },
                    { 1, _armoredEnemiesList },
                    { 2, _vikingEnemiesList }
                };
            }
            return _enmies;
        }
        private set { }
    }

    [SerializeField]
    private List<GameObject> _normalEnemiesList;
    [SerializeField]
    private List<GameObject> _armoredEnemiesList;
    [SerializeField]
    private List<GameObject> _vikingEnemiesList;

    [SerializeField]
    private List<GameObject> _bossList;
    public List<GameObject> BossList => _bossList;

    [SerializeField]
    private List<GameObject> _generalList;
    public List<GameObject> GeneralList => _bossList;

    [SerializeField]
    private GameObject _normalRewardPrefab;
    public GameObject NormalRewardPrefab => _normalRewardPrefab;

    [SerializeField]
    private GameObject _bossRewardPrefab;
    public GameObject BossRewardPrefab => _bossRewardPrefab;

    [SerializeField]
    private ResourceGeneratePatternDataSO _resourceGeneratePatternDataSO;
    public ResourceGeneratePatternDataSO ResourceGeneratePatternDataSO => _resourceGeneratePatternDataSO;
}
