using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundConfigurer : ComingObjectConfigurer
{
    public override ComingElements SetComingObjectElements(Transform groundTrm, bool isRaft = false)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer(groundTrm,
                                                              _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray(),
                                                              _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray());

        ResourceConfigurer resourceConfigurer = new ResourceConfigurer(groundTrm,
                                                                       _comingElementsDataSO.ResourceGeneratePatternDataSO.ResourceGeneratePatterns.ToArray());

        RewardConfigurer rewardConfigurer = new RewardConfigurer(groundTrm,
                                                                 _comingElementsDataSO.NormalRewardPrefab.name,
                                                                 _comingElementsDataSO.BossRewardPrefab.name);

        return new GroundElements(enemyConfigurer.SetEnemy(_previousElementsPositions, isRaft),
                                  resourceConfigurer.SetResource(_previousElementsPositions),
                                  rewardConfigurer.SetReward(_previousElementsPositions));
    }
}