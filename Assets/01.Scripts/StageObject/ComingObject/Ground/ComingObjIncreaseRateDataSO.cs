using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingObj")]
public class ComingObjIncreaseRateDataSO : ScriptableObject
{
    [SerializeField]
    [Tooltip("�ִ� curWave * commonEnemyIncreaseRate�� ���� �� ��ŭ ���� �´�.")]
    private int commonEnemyIncreaseRate;
    public int CommonEnemyIncreaseRate => commonEnemyIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * bossEnemyIncreaseRate�� ���� �� ��ŭ ���� �´�.")]
    private int bossEnemyIncreaseRate;
    public int BossEnemyIncreaseRate => bossEnemyIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ ���ϰ� �´�.")]
    private int groundIncreaseRate;
    public int GroundIncreaseRate => groundIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ Raft(������� ����)�� �´�.")]
    private int raftIncreaseRate;
    public int RaftIncreaseRate => raftIncreaseRate;

}
