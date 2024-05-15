using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using ArmySystem;




public delegate void ChangedArmy(Army prevArmy, Army newArmy);
public delegate void ChangedBattleMode(MovefocusMode mode);

public delegate void ModifyArmyInfo();

public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseStartPriorityEvent();
public delegate void BattlePhaseEndEvent();
public delegate void GroundArrivedEvent();

public delegate void StartQuestEvent();
public delegate void EndQuestEvent();
public delegate void ProgressQuestEvent();

public delegate void OffPopUiEvent();

public delegate void ViewNoiseIncreaseEvent();
public delegate void NoiseIncreaseEvent();

public static class SignalHub
{

    /// <summary>
    /// 적 죽을 때 실행됨
    /// </summary>
    public static EnemyPenguinDead OnEnemyPenguinDead;

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
    public static ModifyArmyInfo OnModifyCurArmy;

    #endregion

    #region 웨이브
    public static BattlePhaseStartEvent OnBattlePhaseStartEvent;
    public static BattlePhaseStartPriorityEvent OnBattlePhaseStartPriorityEvent;
    public static BattlePhaseEndEvent OnBattlePhaseEndEvent;
    public static GroundArrivedEvent OnGroundArrivedEvent;
    #endregion

    #region Quest

    public static StartQuestEvent OnStartQuestEvent;
    public static EndQuestEvent OnEndQuestEvent;
    public static ProgressQuestEvent OnProgressQuestEvent;

    public static OffPopUiEvent OnOffPopUiEvent;

    #endregion

    #region Noise

    public static ViewNoiseIncreaseEvent OnViewNoiseIncreaseEvent;
    public static NoiseIncreaseEvent OnNoiseIncreaseEvent;

    #endregion
}
