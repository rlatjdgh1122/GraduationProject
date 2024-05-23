using UnityEngine;
using ArmySystem;


public delegate void OnValueChanged<T>(T prevValue , T curValue);
public delegate void OnValueUpdated<T>(T value);

public delegate void ChangedArmy(Army prevArmy, Army newArmy);
public delegate void ChangedBattleMode(MovefocusMode mode);

public delegate void ModifyArmyInfo();

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

public delegate void LockBuyPenguinButtonEvent();

public static class SignalHub
{

    /// <summary>
    /// �� ���� �� �����
    /// </summary>
    public static EnemyPenguinDead OnEnemyPenguinDead;

    #region ����
    /// <summary>
    /// 1�� 2�� ���� ���� ����� �� ȣ��
    /// </summary>
    public static ChangedArmy OnArmyChanged;
    /// <summary>
    /// AŰ�� ���� ��Ʋ��带 �ٲ� �� ȣ��
    /// </summary>
    public static ChangedBattleMode OnBattleModeChanged;
    /// <summary>
    /// ���ܿ� �߰� �ǰų� ���� �Ǵ� �� ���ܿ� ������ ������ �� ȣ��
    /// </summary>
    public static ModifyArmyInfo OnModifyCurArmy;

    #endregion

    #region ���̺�
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

    public static LockBuyPenguinButtonEvent OnLockButtonEvent;

    public static OffPopUiEvent OnOffPopUiEvent;


    #region Noise

    public static ViewNoiseIncreaseEvent OnViewNoiseIncreaseEvent;
    public static NoiseIncreaseEvent OnNoiseIncreaseEvent;
    public static NoiseUpdateEvent OnNoiseUpdateEvent;

    #endregion
}
