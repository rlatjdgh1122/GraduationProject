using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Resource")]
public class ResourceStat : BaseStat
{
    [Header("Resource Info")]
    public int requiredWorkerCount; //자원 채굴에 필요한 최소 일꾼 갯수

    [Header("Receive Count")]
    public int receiveCountAtOnce; //한번 캘 때마다 주는 자원 갯수
    public int receiveCountWhenCompleted; //다 캐면 마지막으로 주는 자원 갯수

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
}
