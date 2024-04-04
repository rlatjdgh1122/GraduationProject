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
    public QuestGoalType[] QuestGoalType;

    public string Id;

    public int QuestGoalCount; // 퀘스트 완료 조건의 개수 (ex: 원거리적도 3마리 잡고, 근거리 적도 3마리 잡아야 하면 2)
    public int[] RequiredAmount; // 퀘스트 완료 조건은 여러개일 수 있으니 조건들의 반복횟수(근거리 3마리 잡기면 3)는 배열로

    public string[] GoalIds; // 퀘스트 완료 조건은 여러개일 수 있으니 조건들의 id는 배열로

    public bool IsTutorialQuest;
    public int TutorialQuestIdx;

    [TextArea()]
    public string[] TutorialTexts;

    public QuestUIData QuestUIDataInfo;
    public QuestRewardData QuestRewardInfo;
}
