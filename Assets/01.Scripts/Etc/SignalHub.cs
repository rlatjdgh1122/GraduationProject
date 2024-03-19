using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct ArrangementInfo
{
    public int Legion;
    public int SlotIdx;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
}

[System.Serializable]
public enum MovefocusMode
{
    Command = 0, //내말을 잘들음
    Battle, //전투광이 됨

}

public delegate void ChangedArmy(Army prevArmy, Army newArmy);
public delegate void ChagnedUILegion(int prevLegion, int newLegion);
public delegate void PenguinArrangementSetting(ArrangementInfo info);
public delegate void ChangedBattleMode(MovefocusMode mode);

public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseEndEvent();
public delegate void IceArrivedEvent();

public static class SignalHub
{
    public static ChangedArmy OnArmyChanged;
    public static PenguinArrangementSetting OnArrangementInfoModify;
    public static ChangedBattleMode OnBattleModeChanged;
    public static ChagnedUILegion OnUILegionChanged;

    public static EnemyPenguinDead OnEnemyPenguinDead;

    public static BattlePhaseStartEvent OnBattlePhaseStartEvent;
    public static BattlePhaseEndEvent OnBattlePhaseEndEvent;
    public static IceArrivedEvent OnIceArrivedEvent;
}
