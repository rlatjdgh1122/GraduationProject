using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestGoalType// ����Ʈ ��ǥ�� ����
{
    Kill,
    Gather
}

public class QuestGoal
{
    public QuestGoalType GoalType;
    public int RequiredAmount;
    public int CurrentAmount;

    // ��ǥ ������
    public QuestGoal(QuestGoalType type, int amount)
    {
        GoalType = type;
        RequiredAmount = amount;
        CurrentAmount = 0;
    }

    // ��ǥ �޼� ���� Ȯ��
    public bool IsCompleted()
    {
        return CurrentAmount >= RequiredAmount;
    }
}
