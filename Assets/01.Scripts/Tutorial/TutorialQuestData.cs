using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TutorialQuestData
{
    public bool IsTutorialQuest;
    public int TutorialQuestIdx;

    [TextArea()]
    public string[] TutorialTexts;
}
