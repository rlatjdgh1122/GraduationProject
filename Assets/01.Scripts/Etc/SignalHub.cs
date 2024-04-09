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
public delegate void ChangedArmyScreen(Army prevArmy, Army newArmy);
public delegate void PenguinArrangementSetting(ArrangementInfo info);
public delegate void ChangedBattleMode(MovefocusMode mode);

public delegate void ModifyArmyInfo();

public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseEndEvent();
public delegate void IceArrivedEvent();
public delegate void DummyPenguinsCompletedGoToHouseEvent();

public delegate void StartQuestEvent();
public delegate void EndQuestEvent();
public delegate void ProgressQuestEvent();

public delegate void OffPopUiEvent();

public static class SignalHub
{
    /// <summary>
    /// 더미 펭귄이 모두 성공적으로 안에 들어갔다면
    /// </summary>
    public static DummyPenguinsCompletedGoToHouseEvent OnCompletedGoToHouseEvent;

    /// <summary>
    /// 적 죽을 때 실행됨
    /// </summary>
    public static EnemyPenguinDead OnEnemyPenguinDead;

    /// <summary>
    /// F1~F9눌러 군단 화면 고정 키 변경
    /// </summary>
    public static ChangedArmyScreen OnArmyScreenChanged;

    #region 군단
    /// <summary>
    /// 1번 2번 눌러 군단 변경될 때 호출
    /// </summary>
    public static ChangedArmy OnArmyChanged;
    /// <summary>
    /// A키를 눌러 배틀모드를 바꿀 때 호출
    /// </summary>
    public static ChangedBattleMode OnBattleModeChanged;
    /// <summary>
    /// 군단에 추가 되거나 삭제 되는 둥 군단에 정보가 수정될 때 호출
    /// </summary>
    public static ModifyArmyInfo OnModifyArmyInfo;

    #endregion

    #region 군단 UI
    public static PenguinArrangementSetting OnModifyArrangementInfo;
    #endregion

    #region 웨이브
    public static BattlePhaseStartEvent OnBattlePhaseStartEvent;
    public static BattlePhaseEndEvent OnBattlePhaseEndEvent;
    public static IceArrivedEvent OnIceArrivedEvent;
    #endregion

    #region Quest

    public static StartQuestEvent OnStartQuestEvent;
    public static EndQuestEvent OnEndQuestEvent;
    public static ProgressQuestEvent OnProgressQuestEvent;

    public static OffPopUiEvent OnOffPopUiEvent;

    #endregion


}
