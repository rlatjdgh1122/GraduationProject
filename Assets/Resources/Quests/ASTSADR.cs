using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTSADR : MonoBehaviour
{
    [SerializeField]
    private QuestInfoSO _questInfo;
    private QuestState _curQuestState;

    private string questId;

    private void Awake()
    {
        questId = _questInfo.id;
    }

    private void Start()
    {
        QuestManager.Instance.QuestEventsCompo.OnQuestStateChange += QuestStepStateChange;
    }

    private void QuestStepStateChange(Quest quest)
    {
        if (quest.QuestInfo.id.Equals(_questInfo.id))
        {
            _curQuestState = quest.State;
            Debug.Log($"State:{quest.QuestInfo.id}: {_curQuestState}");
        }
    }

    private void OnDisable()
    {
        QuestManager.Instance.QuestEventsCompo.OnQuestStateChange -= QuestStepStateChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_curQuestState.Equals(QuestState.CANSTART))
            {
                QuestManager.Instance.QuestEventsCompo.StartQuest(_questInfo);
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            QuestManager.Instance.QuestEventsCompo.FinishQuest(questId);
        }
    }
}
