using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents
{
    public event Action<string> OnStartQuest;
    public void StartQuest(QuestInfoSO quesInfo) //Ω√¿€
    {
        if (TutorialManager.Instance.CurQuestIdx == quesInfo.QuestIDX)
        {
            OnStartQuest?.Invoke(quesInfo.id);
        }
        else
        {
            Debug.LogError($"{quesInfo.id}'s {quesInfo.QuestIDX} is not {TutorialManager.Instance.CurQuestIdx}");
        }
    }

    public event Action<string> OnAdvanceQuest; 
    public void AdvanceQuest(string id)
    {
        OnAdvanceQuest?.Invoke(id);
    }

    public event Action<string> OnFinishQuest; //≥° 
    public void FinishQuest(string id)
    {
        OnFinishQuest?.Invoke(id);
        TutorialManager.Instance.IncreaseQuestIdx();
    }

    public event Action<Quest> OnQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        OnQuestStateChange?.Invoke(quest);
    }

    public event Action<string, int, QuestStepState> OnQuestStepStateChange;
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        OnQuestStepStateChange?.Invoke(id, stepIndex, questStepState);
    }
}
