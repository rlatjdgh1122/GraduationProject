using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaftConfigurer : ComingObjectConfigurer
{
    public override ComingElements SetComingObjectElements(Transform groundTrm, bool isRaft = true)
    {
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer
        (
            groundTrm,
            _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray(),
            _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray()
        );

        return new RaftElements(enemyConfigurer.SetEnemy(_previousElementsPositions, isRaft));
    }
}
