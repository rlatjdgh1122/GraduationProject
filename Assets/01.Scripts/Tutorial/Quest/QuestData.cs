using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum QuestState
{
    BeforeStart,
    Running,
    Finish
}

[Serializable]
public class QuestData
{
    public QuestData(QuestData questData)
    {
        IsTutorialQuest = questData.IsTutorialQuest;
        QuestStateEnum = questData.QuestStateEnum;
        TutorialQuestIdx = questData.TutorialQuestIdx;
        RepeatCount = questData.RepeatCount;

        Id = questData.Id;

        TutorialTexts = questData.TutorialTexts;

        QuestUIDataInfo = questData.QuestUIDataInfo;
        QuestRewardInfo = questData.QuestRewardInfo;
    }

    public QuestState QuestStateEnum;

    public int RepeatCount;

    public string Id;

    public bool IsTutorialQuest;
    public int TutorialQuestIdx;
    public string[] TutorialTexts;

    public int CurProgressCount = 0;


    public QuestUIData QuestUIDataInfo;
    public QuestRewardData QuestRewardInfo;
}
