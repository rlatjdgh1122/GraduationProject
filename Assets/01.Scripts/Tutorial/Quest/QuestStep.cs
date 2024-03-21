using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished;
    private string questId;
    private int questStepIdx;

    public void InitQuestStep(string questStepState, string id, int questStepIdx)
    {
        if (questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }

        this.questId = id;
        this.questStepIdx = questStepIdx;

        Debug.Log($"questId: {questId}");
        Debug.Log($"questStepIdx: {questStepIdx}");
    }

    protected void FinishQuest() //이거 하고  QuestManager.Instance.QuestEventsCompo.FinishQuest(questId);하면 퀘스트 클리어
    {
        if(!isFinished)
        {
            isFinished = true;
            QuestManager.Instance.QuestEventsCompo.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        QuestManager.Instance.QuestEventsCompo.QuestStepStateChange(questId, questStepIdx, 
                                                                 new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);
}
