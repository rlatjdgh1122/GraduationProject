using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    [SerializeField]
    private QuestInfoSO _questInfo;
    public QuestInfoSO QuestInfo => _questInfo;

    public QuestState State;

    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO questInfo)
    {
        this._questInfo = questInfo;
        this.State = QuestState.REQUIREMENTSNOTMET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[_questInfo.questStepPrefabs.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this._questInfo = questInfo;
        this.State = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepFinished()
    {
        return currentQuestStepIndex < _questInfo.questStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitQuestStep(questStepStates[currentQuestStepIndex].state, _questInfo.id, currentQuestStepIndex);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        questStepPrefab = _questInfo.questStepPrefabs[currentQuestStepIndex];
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(State, currentQuestStepIndex, questStepStates);
    }
}
