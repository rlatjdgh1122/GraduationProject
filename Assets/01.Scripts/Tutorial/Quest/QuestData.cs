using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        RepeatCount = questData.RepeatCount;

        Id = questData.Id;

        QuestTexts = questData.QuestTexts;
    }

    public bool isFinQuest;
    public bool isStartedQuest;

    public int RepeatCount;

    public string Id;

    public bool IsTutorialQuest;
    public int TutorialQuestIdx;
    public string[] QuestTexts;
}
