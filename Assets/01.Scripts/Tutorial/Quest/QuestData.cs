using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum QuestState
{
    Locked,
    CanStart,
    Running,
    CanFinish,
    Finish
}

[Serializable]
public class QuestData
{
    public QuestState QuestStateEnum;

    public string Id;

    public QuestGoalData[] QuestGoalInfo;

    public TutorialQuestData TutorialQuestInfo;

    public QuestRewardData QuestRewardInfo;
}
