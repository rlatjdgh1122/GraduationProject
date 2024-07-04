using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundConfigurer : ComingObjectConfigurer
{
    public override ComingElements SetComingObjectElements(Transform groundTrm)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer(groundTrm,
                                                              GetSpawnEnemis(),
                                                              _comingElementsDataSO.BossList.Select(prefab => prefab.name).ToArray(),
                                                              _comingObjIncreaseRateDataSO,
                                                              _enemyArmySpawnPatternsSO);

        ResourceConfigurer resourceConfigurer = new ResourceConfigurer(groundTrm,
                                                                       _comingElementsDataSO.ResourceGeneratePatternDataSO.ResourceGeneratePatterns.ToArray());

        RewardConfigurer rewardConfigurer = new RewardConfigurer(groundTrm,
                                                                 _comingElementsDataSO.NormalRewardPrefab.name,
                                                                 _comingElementsDataSO.BossRewardPrefab.name);

        return new GroundElements(enemyConfigurer.SetEnemy(_previousElementsPositions),
                                  resourceConfigurer.SetResource(_previousElementsPositions),
                                  rewardConfigurer.SetReward(_previousElementsPositions));
    }
}
