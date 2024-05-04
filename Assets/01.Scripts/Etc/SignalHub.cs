using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using ArmySystem;




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
    /// ���� ����� ��� ���������� �ȿ� ���ٸ�
    /// </summary>
    public static DummyPenguinsCompletedGoToHouseEvent OnCompletedGoToHouseEvent;

    /// <summary>
    /// �� ���� �� �����
    /// </summary>
    public static EnemyPenguinDead OnEnemyPenguinDead;

    /// <summary>
    /// F1~F9���� ���� ȭ�� ���� Ű ����
    /// </summary>
    public static ChangedArmyScreen OnArmyScreenChanged;

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

    #region ���� UI
    public static PenguinArrangementSetting OnModifyArrangementInfo;
    #endregion

    #region ���̺�
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
