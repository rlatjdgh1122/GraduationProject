using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingObj")]
public class ComingObjIncreaseRateDataSO : ScriptableObject
{
    [SerializeField]
    [Tooltip("�ִ� curWave * commonEnemyIncreaseRate�� ���� �� ��ŭ ���� �´�.")]
    private float commonEnemyIncreaseRate;
    public float CommonEnemyIncreaseRate => commonEnemyIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * bossEnemyIncreaseRate�� ���� �� ��ŭ ���� �´�.")]
    private float bossEnemyIncreaseRate;
    public float BossEnemyIncreaseRate => bossEnemyIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ ���ϰ� �´�.")]
    private float groundIncreaseRate;
    public float GroundIncreaseRate => groundIncreaseRate;

    [SerializeField]
    [Tooltip("�ִ� curWave * groundIncreaseRate�� ���� �� ��ŭ Raft(������� ����)�� �´�.")]
    private float raftIncreaseRate;
    public float RaftIncreaseRate => raftIncreaseRate;

}
