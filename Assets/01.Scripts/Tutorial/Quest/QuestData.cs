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

    public int QuestGoalCount; // ����Ʈ �Ϸ� ������ ���� (ex: ���Ÿ����� 3���� ���, �ٰŸ� ���� 3���� ��ƾ� �ϸ� 2)
    public int[] RequiredAmount; // ����Ʈ �Ϸ� ������ �������� �� ������ ���ǵ��� �ݺ�Ƚ��(�ٰŸ� 3���� ���� 3)�� �迭��

    public string[] GoalIds; // ����Ʈ �Ϸ� ������ �������� �� ������ ���ǵ��� id�� �迭��

    public bool IsTutorialQuest;
    public int TutorialQuestIdx;

    [TextArea()]
    public string[] TutorialTexts;

    public QuestUIData QuestUIDataInfo;
    public QuestRewardData QuestRewardInfo;
}
