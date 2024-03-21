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

public delegate void ModifyArmyInfo();

public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseEndEvent();
public delegate void IceArrivedEvent();

public static class SignalHub
{
    public static EnemyPenguinDead OnEnemyPenguinDead;

    #region 군단
    //1번 2번 눌러 군단 변경될 때 호출
    public static ChangedArmy OnArmyChanged;
    //A키를 눌러 배틀모드를 바꿀 때 호출
    public static ChangedBattleMode OnBattleModeChanged;
    //군단에 추가 되거나 삭제 되는 둥 군단에 정보가 수정될 때 호출
    public static ModifyArmyInfo OnModifyArmyInfo;

    #endregion

    #region 군단 UI
    public static PenguinArrangementSetting OnModifyArrangementInfo;
    public static ChagnedUILegion OnUILegionChanged;
    #endregion

    #region 웨이브
    public static BattlePhaseStartEvent OnBattlePhaseStartEvent;
    public static BattlePhaseEndEvent OnBattlePhaseEndEvent;
    public static IceArrivedEvent OnIceArrivedEvent;
    #endregion
}
