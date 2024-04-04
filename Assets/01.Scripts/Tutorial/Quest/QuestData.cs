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
    public QuestGoalType QuestGoalType;

    public int RequiredAmount;

    public string Id;

    public bool IsTutorialQuest;
    public int TutorialQuestIdx;

    [TextArea()]
    public string[] TutorialTexts;

    public QuestUIData QuestUIDataInfo;
    public QuestRewardData QuestRewardInfo;
}
