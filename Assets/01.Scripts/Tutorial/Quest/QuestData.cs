using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    public QuestData(QuestData questData)
    {
        IsTutorialQuest = questData.IsTutorialQuest;
        isFinQuest = questData.isFinQuest;
        isStartedQuest = questData.isStartedQuest;
        TutorialQuestIdx = questData.TutorialQuestIdx;
        RepetCount = questData.RepetCount;

        Id = questData.Id;

        QuestTexts = questData.QuestTexts;
        QuestObj = questData.QuestObj;
    }

    public bool IsTutorialQuest;
    public bool isFinQuest;
    public bool isStartedQuest;

    public int TutorialQuestIdx;
    public int RepetCount;

    public string Id;

    public string[] QuestTexts;
    public GameObject QuestObj;
}
