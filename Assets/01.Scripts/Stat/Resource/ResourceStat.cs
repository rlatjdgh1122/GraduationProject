using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Resource")]
public class ResourceStat : BaseStat
{
    [Header("Resource Info")]
    public int requiredWorkerCount; //�ڿ� ä���� �ʿ��� �ּ� �ϲ� ����

    [Header("Receive Count")]
    public int receiveCountAtOnce; //�ѹ� Ķ ������ �ִ� �ڿ� ����
    public int receiveCountWhenCompleted; //�� ĳ�� ���������� �ִ� �ڿ� ����

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
}
