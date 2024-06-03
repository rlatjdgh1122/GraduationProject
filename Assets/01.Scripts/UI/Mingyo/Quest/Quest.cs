using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestState QuestStateEnum; // ����Ʈ�� ����

    public string QuestId;

    public List<QuestGoal> QuestGoalList { get; private set; } = new(); // �� ����Ʈ�� �Ϸ� �ϱ� ���� ��ǥ��
    public QuestData QuestDataCompo { get; private set; }

    public Quest(QuestData questData) // ����Ʈ ������
    {
        QuestStateEnum = questData.QuestStateEnum;

        QuestId = questData.Id;
        QuestDataCompo = questData;
    }

    public void UpdateCondition(string id) // ����Ʈ�� ����Ǿ����� 
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
