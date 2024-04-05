using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuestGoalData
{
    public int[] RequiredAmount; // 퀘스트 완료 조건은 여러개일 수 있으니 조건들의 반복횟수(근거리 3마리 잡기면 3)는 배열로
    public string[] GoalIds; // 퀘스트 완료 조건은 여러개일 수 있으니 조건들의 id는 배열로
    public QuestGoalType[] QuestGoalType;
}
