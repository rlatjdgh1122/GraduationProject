using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuestData
{
    public QuestState state;
    public int questStepIdx;
    public QuestStepState[] questStepStates;

    public QuestData(QuestState state, int questStepIdx, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.questStepIdx = questStepIdx;
        this.questStepStates = questStepStates;
    }
}
