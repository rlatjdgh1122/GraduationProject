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
    Command = 0, //������ �ߵ���
    Battle, //�������� ��

}

public delegate void ChangedArmy(Army prevArmy, Army newArmy);
public delegate void ChangedArmyScreen(Army prevArmy, Army newArmy);
public delegate void ChagnedUILegion(int prevLegion, int newLegion);
public delegate void PenguinArrangementSetting(ArrangementInfo info);
public delegate void ChangedBattleMode(MovefocusMode mode);

public delegate void ModifyArmyInfo();

public delegate void EnemyPenguinDead();

public delegate void BattlePhaseStartEvent();
public delegate void BattlePhaseEndEvent();
public delegate void IceArrivedEvent();

public delegate void StartQuestEvent();
public delegate void EndQuestEvent();
public delegate void ProgressQuestEvent();

public delegate void OffPopUiEvent();

public static class SignalHub
{
    public static EnemyPenguinDead OnEnemyPenguinDead;
    //F1~F9���� ���� ȭ�� ���� Ű ����
    public static ChangedArmyScreen OnArmyScreenChanged;
    #region ����
    //1�� 2�� ���� ���� ����� �� ȣ��
    public static ChangedArmy OnArmyChanged;
    //AŰ�� ���� ��Ʋ��带 �ٲ� �� ȣ��
    public static ChangedBattleMode OnBattleModeChanged;
    //���ܿ� �߰� �ǰų� ���� �Ǵ� �� ���ܿ� ������ ������ �� ȣ��
    public static ModifyArmyInfo OnModifyArmyInfo;

    #endregion

    #region ���� UI
    public static PenguinArrangementSetting OnModifyArrangementInfo;
    public static ChagnedUILegion OnUILegionChanged;
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
