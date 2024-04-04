using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestState QuestStateEnum;

    public string QuestId;

    public List<QuestGoal> QuestGoalList = new List<QuestGoal>(); // ∏Ò«•
    public QuestData QuestDataCompo { get; private set; }

    public Quest(QuestData questData)
    {
        QuestStateEnum = questData.QuestStateEnum;

        QuestId = questData.Id;
        QuestDataCompo = questData;
    }

    public void SetQuestState(QuestState questState)
    {
        QuestStateEnum = questState;
    }

    public bool IsCompleted()
    {
        foreach (QuestGoal goal in QuestGoalList)
        {
            if (!goal.IsCompleted()) { return false; }
        }
        return true;
    }
}
