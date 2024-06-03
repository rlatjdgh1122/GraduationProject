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
    public QuestState QuestStateEnum; // 이것도 퀘스트 상태이긴 한데 이 퀘스트가 실행가능 한지

    public string Id;

    public QuestGoalData[] QuestGoalInfo;

    public TutorialQuestData TutorialQuestInfo;

    public QuestRewardData QuestRewardInfo;
}
