using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ComingObjectConfigurer : MonoBehaviour
{
    [SerializeField]
    protected ComingElementsDataSO _comingElementsDataSO;

    [SerializeField]
    protected EnemyArmySpawnPatternsSO _enemyArmySpawnPatternsSO;

    protected List<Vector3> _previousElementsPositions = new List<Vector3>();

    public virtual ComingElements SetComingObjectElements(Transform groundTrm)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer(groundTrm,
                                                              GetSpawnEnemis(),
                                                              _comingElementsDataSO.BossList.Select(prefab => prefab.name).ToArray(),
                                                              _enemyArmySpawnPatternsSO);

        //ResourceConfigurer resourceConfigurer = new ResourceConfigurer(groundTrm,
        //                                                               _comingElementsDataSO.ResourceGeneratePatternDataSO.ResourceGeneratePatterns.ToArray());

        //RewardConfigurer rewardConfigurer = new RewardConfigurer(groundTrm,
        //                                                         _comingElementsDataSO.NormalRewardPrefab.name,
        //                                                         _comingElementsDataSO.BossRewardPrefab.name);

        return new ComingElements(enemyConfigurer.SetEnemy(_previousElementsPositions));
    }

    protected string[] GetSpawnEnemis()
    {
        int spawnEnemisType = (int)(WaveManager.Instance.CurrentWaveCount * 0.2f);

        return _comingElementsDataSO.Enmies[spawnEnemisType].Select(prefab => prefab.name).ToArray();
    }
}
