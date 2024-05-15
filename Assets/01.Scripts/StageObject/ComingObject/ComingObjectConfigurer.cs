using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ComingObjectConfigurer : MonoBehaviour
{
    [SerializeField]
    protected ComingElementsDataSO _comingElementsDataSO;

    protected List<Vector3> _previousElementsPositions = new List<Vector3>();

    public virtual ComingElements SetComingObjectElements(Transform groundTrm, bool isRaft = false)
    {
        Debug.Log($"{gameObject} ComingObjClear");
        _previousElementsPositions.Clear();

        EnemyConfigurer enemyConfigurer = new EnemyConfigurer(groundTrm,
                                                              _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray(),
                                                              _comingElementsDataSO.EnemiesList.Select(prefab => prefab.name).ToArray());

        //ResourceConfigurer resourceConfigurer = new ResourceConfigurer(groundTrm,
        //                                                               _comingElementsDataSO.ResourceGeneratePatternDataSO.ResourceGeneratePatterns.ToArray());

        //RewardConfigurer rewardConfigurer = new RewardConfigurer(groundTrm,
        //                                                         _comingElementsDataSO.NormalRewardPrefab.name,
        //                                                         _comingElementsDataSO.BossRewardPrefab.name);

        return new ComingElements(enemyConfigurer.SetEnemy(_previousElementsPositions, isRaft));
    }
}
