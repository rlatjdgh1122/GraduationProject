using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestGoalType// 퀘스트 목표의 유형
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

    // 목표 생성자
    public QuestGoal(QuestGoalType type, int amount, string goalID)
    {
        GoalType = type;
        RequiredAmount = amount;
        CurrentAmount = 0;
        GoalID = goalID;
    }

    // 목표 달성 여부 확인
    public bool IsCompleted()
    {
        return CurrentAmount >= RequiredAmount;
    }
}
