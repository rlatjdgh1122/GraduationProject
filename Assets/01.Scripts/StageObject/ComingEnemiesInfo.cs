using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ComingEnemiesInfo
{
    public Ground[] ComingGrounds { get; private set; }
    public int ComingGroundsCount;
    //public int ComingBoatCount;
    // ��

    public ComingEnemiesInfo(Ground[] comingGrounds)
    {
        ComingGrounds = comingGrounds;
        ComingGroundsCount = ComingGrounds.Length;
    }
}
