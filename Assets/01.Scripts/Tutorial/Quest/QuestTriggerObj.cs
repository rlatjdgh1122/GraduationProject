using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface QuestTriggerObj
{
    public string QuestId { get; set; }

    protected void StartQuest()
    {
        QuestManager.Instance.StartQuest(QuestId);
    }

    protected void StartTutorialQuest()
    {
        QuestManager.Instance.StartTutorial(QuestId);
    }

    protected void ProgressQuest()
    {
        QuestManager.Instance.ProgressQuest(QuestId);
    }
}
