using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestGoalType// ����Ʈ ��ǥ�� ����
{
    Kill,
    Gather,
    Upgrade,
    Install,
    BattleWin
}

public class QuestGoal
{
    public QuestGoalType GoalType;
    public int RequiredAmount;
    public int CurrentAmount;

    public string GoalID;

    // ��ǥ ������
    public QuestGoal(QuestGoalType type, int amount, string goalID)
    {
        GoalType = type;
        RequiredAmount = amount;
        CurrentAmount = 0;
        GoalID = goalID;
    }

    // ��ǥ �޼� ���� Ȯ��
    public bool IsCompleted()
    {
        return CurrentAmount >= RequiredAmount;
    }
}
