using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuestGoalData
{
    public int[] RequiredAmount; // ����Ʈ �Ϸ� ������ �������� �� ������ ���ǵ��� �ݺ�Ƚ��(�ٰŸ� 3���� ���� 3)�� �迭��
    public string[] GoalIds; // ����Ʈ �Ϸ� ������ �������� �� ������ ���ǵ��� id�� �迭��
    public QuestGoalType[] QuestGoalType;
}
