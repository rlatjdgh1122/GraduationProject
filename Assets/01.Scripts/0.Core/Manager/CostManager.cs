using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : Singleton<CostManager>
{
    //�ӽ÷� ����� ���߿� �ּ� Ǯ��
    public int CurrentCost; // { get; private set; }

    public void ChangeCost(int cost)
    {
        CurrentCost = cost;
    }
}
