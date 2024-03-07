using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArrangementInfo
{
    public int legion;
    public int SlotIdx;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
}

public delegate void ChangedArmy(Army prevArmy, Army newArmy);

public delegate void PenguinArrangementSetting(ArrangementInfo info);
public delegate void ChangedBattleMode(bool isBattleMode);
public static class SignalHub
{
    public static ChangedArmy OnArmyChanged;
    public static PenguinArrangementSetting OnArrangementInfoModify;
    public static ChangedBattleMode OnBattleModeChanged;

}
