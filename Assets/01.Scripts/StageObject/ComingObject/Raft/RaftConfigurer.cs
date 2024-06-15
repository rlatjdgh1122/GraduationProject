using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaftConfigurer : ComingObjectConfigurer
{
    public override ComingElements SetComingObjectElements(Transform groundTrm)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer
        (
            groundTrm,
            _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray(),
            _comingElementsDataSO.BossList.Select(prefab => prefab.name).ToArray(),
            _comingObjIncreaseRateDataSO
        );

        return new RaftElements(enemyConfigurer.SetEnemy(_previousElementsPositions));
    }
}
