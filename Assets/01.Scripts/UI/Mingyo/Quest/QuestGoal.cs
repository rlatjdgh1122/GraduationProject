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
    public QuestGoalType GoalType { get; private set; }
    public int RequiredAmount { get; private set; }
    public int CurrentAmount { get; private set; }

    public string GoalID;

    // ��ǥ ������
    public QuestGoal(QuestGoalType type, int amount, string goalID)
    {
        GoalType = type;
        RequiredAmount = amount;
        CurrentAmount = 0;
        GoalID = goalID;
    }

    public void IncreaseCurrentAmountValue()
    {
        CurrentAmount++;
    }

    // ��ǥ �޼� ���� Ȯ��
    public bool IsCompleted()
    {
        return CurrentAmount >= RequiredAmount;
    }
}
