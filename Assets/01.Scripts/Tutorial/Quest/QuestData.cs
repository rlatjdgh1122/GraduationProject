using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    public bool IsTutorialQuest;
    public bool isFinQuest = false;
    public bool isStartedQuest = false;

    public int TutorialQuestIdx;
    public int RepetCount;

    public string Id;

    public string[] QuestTexts;
    public GameObject QuestObj;
}
