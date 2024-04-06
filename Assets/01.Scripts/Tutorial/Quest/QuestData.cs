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

    public int QuestGoalCount; // ����Ʈ �Ϸ� ������ ���� (ex: ���Ÿ����� 3���� ���, �ٰŸ� ���� 3���� ��ƾ� �ϸ� 2)

    public QuestGoalData[] QuestGoalInfo;

    public TutorialQuestData TutorialQuestInfo;

    public QuestRewardData QuestRewardInfo;
}
