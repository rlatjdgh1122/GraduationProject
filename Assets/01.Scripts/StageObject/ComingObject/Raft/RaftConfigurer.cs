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
            GetSpawnEnemis(),
            null,
            null,
            _enemyArmySpawnPatternsSO
        );

        return new RaftElements(enemyConfigurer.SetEnemy(_previousElementsPositions, true));
    }
}
