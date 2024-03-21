using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public QuestEvents QuestEventsCompo;

    private Dictionary<string, Quest> _questDic = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        _questDic = CreateQuestDic();

    }

    private void OnEnable()
    {
        QuestEventsCompo = new();
        QuestEventsCompo.OnStartQuest += StartQuest;
        QuestEventsCompo.OnAdvanceQuest += AdvanceQuest;
        QuestEventsCompo.OnFinishQuest += FinishQuest;

        QuestEventsCompo.OnQuestStepStateChange += QuestStepStateChange;

    }

    private void OnDisable()
    {
        QuestEventsCompo.OnStartQuest -= StartQuest;
        QuestEventsCompo.OnAdvanceQuest -= AdvanceQuest;
        QuestEventsCompo.OnFinishQuest -= FinishQuest;


        QuestEventsCompo.OnQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach (Quest quest in _questDic.Values)
        {
            // initialize any loaded quest steps
            if (quest.State == QuestState.ING)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }

            // broadcast the initial state of all quests on startup
            //QuestEventsCompo.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.State = state;
        QuestEventsCompo.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        // start true and prove to be false
        bool meetsRequirements = true;

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.QuestInfo.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).State != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        foreach (Quest quest in _questDic.Values)
        {
            if (quest.State == QuestState.REQUIREMENTSNOTMET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.QuestInfo.id, QuestState.CANSTART);
            }
        }
    }

    private void StartQuest(string id)
    {
        Debug.Log($"Start {id}");
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.QuestInfo.id, QuestState.ING);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        // move on to the next step
        quest.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepFinished())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(quest.QuestInfo.id, QuestState.CANFINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        //ClaimRewards(quest);
        ChangeQuestState(quest.QuestInfo.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest) //나중에 퀘스트 보상 할꺼면 여기에
    {

    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.State);
    }

    private Dictionary<string, Quest> CreateQuestDic()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> questDic2 = new();

        foreach (var quests in allQuests)
        {
            if (questDic2.ContainsKey(quests.id))
            {
                Debug.LogWarning("Duplicate Quest Id" +  quests.id);
            }
            questDic2.Add(quests.id, new Quest(quests));
        }
        return questDic2;
    }

    public Quest GetQuestById(string id)
    {
        Quest quest = _questDic[id];

        if (quest == null)
        {
            Debug.LogError($"Id Not Found: {id}");
        }

        return quest;
    }
}
