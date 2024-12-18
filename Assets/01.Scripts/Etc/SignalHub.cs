using UnityEngine;
using ArmySystem;
using SynergySystem;


public delegate void OnValueChanged<T>(T prevValue , T curValue);
public delegate void OnValueUpdated<T>(T value);

public delegate void ChangedArmy(Army prevArmy, Army newArmy);

public delegate void ModifyArmyInfo();

public delegate void OurPenguinDead();
public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseStartPriorityEvent();
public delegate void BattlePhaseEndEvent();
public delegate void GroundArrivedEvent();
public delegate void RaftArrivedEvent();

public delegate void StartQuestEvent();
public delegate void EndQuestEvent();
public delegate void ProgressQuestEvent();

public delegate void OffPopUiEvent();

public delegate void ViewNoiseIncreaseEvent();
public delegate void NoiseIncreaseEvent();
public delegate void NoiseUpdateEvent();

public delegate void DefaultBuilingClickEvent();
public delegate void ClickMaskingButtonEvent();

public static class SignalHub
{

    /// <summary>
    /// 적 죽을 때 실행됨
    /// </summary>
    public static EnemyPenguinDead OnEnemyPenguinDead;

    /// <summary>
    /// 아군 죽을 때 실행됨
    /// </summary>
    public static OurPenguinDead OnOurPenguinDead;

    #region 군단
    /// <summary>
    /// 1번 2번 눌러 군단 변경될 때 호출
    /// </summary>
    public static ChangedArmy OnArmyChanged;

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
    public static RaftArrivedEvent OnRaftArrivedEvent;
    #endregion

    #region Quest

    public static StartQuestEvent OnStartQuestEvent;
    public static EndQuestEvent OnEndQuestEvent;
    public static ProgressQuestEvent OnProgressQuestEvent;

    public static DefaultBuilingClickEvent OnDefaultBuilingClickEvent;

    public static ClickMaskingButtonEvent OnClickPenguinSpawnButtonEvent;

    #endregion

    public static OffPopUiEvent OnOffPopUiEvent;


    #region Noise

    public static ViewNoiseIncreaseEvent OnViewNoiseIncreaseEvent;
    public static NoiseIncreaseEvent OnNoiseIncreaseEvent;
    public static NoiseUpdateEvent OnNoiseUpdateEvent;

    #endregion


    public static OnValueUpdated<SynergyType> OnSynergyEnableEvent = null;
    public static OnValueUpdated<SynergyType> OnSynergyDisableEvent = null;
}
