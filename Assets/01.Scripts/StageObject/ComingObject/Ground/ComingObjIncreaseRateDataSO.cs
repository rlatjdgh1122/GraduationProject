using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ComingObj")]
public class ComingObjIncreaseRateDataSO : ScriptableObject
{ 
    [SerializeField]
    [Tooltip("최대 curWave * groundIncreaseRate를 내림 한 만큼 빙하가 온다.")]
    private float groundIncreaseRate;
    public float GroundIncreaseRate => groundIncreaseRate;

    [SerializeField]
    [Tooltip("최대 curWave * groundIncreaseRate를 내림 한 만큼 Raft(사라지는 빙하)가 온다.")]
    private float raftIncreaseRate;
    public float RaftIncreaseRate => raftIncreaseRate;

}
