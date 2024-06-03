using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestState QuestStateEnum; // 퀘스트의 상태

    public string QuestId;

    public List<QuestGoal> QuestGoalList { get; private set; } = new(); // 이 퀘스트를 완료 하기 위한 목표들
    public QuestData QuestDataCompo { get; private set; }

    public Quest(QuestData questData) // 퀘스트 생성자
    {
        QuestStateEnum = questData.QuestStateEnum;

        QuestId = questData.Id;
        QuestDataCompo = questData;
    }

    public void UpdateCondition(string id) // 퀘스트가 진행되었을때 
    {
        for(int i = 0; i <  QuestGoalList.Count; i++)
        {
            if (QuestGoalList[i].GoalID == id)
            {
                QuestGoalList[i].IncreaseCurrentAmountValue();
            }
        }
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
