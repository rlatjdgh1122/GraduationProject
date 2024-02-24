using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : Singleton<CostManager>
{
    //임시로 열어둠 나중에 주석 풀기
    public int CurrentCost; // { get; private set; }

    public void ChangeCost(int cost)
    {
        CurrentCost = cost;
    }
}
