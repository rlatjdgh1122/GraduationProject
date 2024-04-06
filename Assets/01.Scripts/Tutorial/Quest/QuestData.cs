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

    public int QuestGoalCount; // 퀘스트 완료 조건의 개수 (ex: 원거리적도 3마리 잡고, 근거리 적도 3마리 잡아야 하면 2)

    public QuestGoalData[] QuestGoalInfo;

    public TutorialQuestData TutorialQuestInfo;

    public QuestRewardData QuestRewardInfo;
}
