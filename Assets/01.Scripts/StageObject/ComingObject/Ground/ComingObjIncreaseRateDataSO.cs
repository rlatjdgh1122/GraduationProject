using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingObj")]
public class ComingObjIncreaseRateDataSO : ScriptableObject
{
    [SerializeField]
    private int commonEnemyIncreaseRate;
    public int CommonEnemyIncreaseRate => commonEnemyIncreaseRate;

    [SerializeField]
    private int bossEnemyCountProportion;
    public int BossEnemyCountProportion => bossEnemyCountProportion;

    [SerializeField]
    [Tooltip("최대 curWave * groundIncreaseRate를 내림 한 만큼 빙하가 온다.")]
    private int groundIncreaseRate;
    public int GroundIncreaseRate => groundIncreaseRate;

    [SerializeField]
    [Tooltip("최대 curWave * groundIncreaseRate를 내림 한 만큼 Raft(작은빙하)가 온다.")]
    private int raftIncreaseRate;
    public int RaftIncreaseRate => raftIncreaseRate;

}
