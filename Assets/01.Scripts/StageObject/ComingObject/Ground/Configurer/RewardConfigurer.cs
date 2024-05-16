using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardConfigurer : BaseElementsConfigurer
{
    private readonly string _normalRewardNames, _bossRewardNames, _magicCageName;

    public RewardConfigurer(Transform transform, string normalRewardNames, string bossRewardNames, string magicCageName) : base(transform)
    {
        _normalRewardNames = normalRewardNames;
        _bossRewardNames = bossRewardNames;
        _magicCageName = magicCageName;
    }

    public CostBox SetReward(List<Vector3> previousElementsPositions)
    {
        if (WaveManager.Instance.CurrentWaveCount == 5)
        {
            Cage cage = PoolManager.Instance.Pop(_magicCageName) as Cage;
            SetGroundElementsPosition(cage.gameObject, transform, previousElementsPositions);
        }

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
