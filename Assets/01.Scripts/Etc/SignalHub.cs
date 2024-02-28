using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArrangementInfo
{
    public int legionIdx;
    public int SlotIdx;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
}

public delegate void ChangedArmy(Army prevArmy, Army newArmy);

public delegate void PenguinArrangementSetting(ArrangementInfo info);
public static class SignalHub
{
    public static ChangedArmy OnArmyChanged;
    public static PenguinArrangementSetting OnArrangementInfoModify;

}
