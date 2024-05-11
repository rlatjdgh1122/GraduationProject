using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardConfigurer : BaseElementsConfigurer
{
    private readonly string _normalRewardNames, _bossRewardNames;

    public RewardConfigurer(Transform transform, string normalRewardNames, string bossRewardNames) : base(transform)
    {
        _normalRewardNames = normalRewardNames;
        _bossRewardNames = bossRewardNames;
    }

    public CostBox SetReward(List<Vector3> previousElementsPositions)
    {
        if (isBossWave)
        {
            CostBox spawnReward = PoolManager.Instance.Pop(_bossRewardNames) as CostBox;

            SetGroundElementsPosition(spawnReward.gameObject, transform, previousElementsPositions);

            return spawnReward;
        }

        if (Random.Range(0, 5) == 0)
        {
            CostBox spawnReward = PoolManager.Instance.Pop(_normalRewardNames) as CostBox;

            SetGroundElementsPosition(spawnReward.gameObject, transform, previousElementsPositions);

            return spawnReward;
        }
        return null;
    }
}
