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
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ ���ϰ� �´�.")]
    private int groundIncreaseRate;
    public int GroundIncreaseRate => groundIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ Raft(��������)�� �´�.")]
    private int raftIncreaseRate;
    public int RaftIncreaseRate => raftIncreaseRate;

}
