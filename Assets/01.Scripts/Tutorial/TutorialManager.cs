using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : Singleton<TutorialManager>
{
    private int curQuestIdx = 0;
    public int CurQuestIdx => curQuestIdx;

    [SerializeField]
    private string[] tutorialIds;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            QuestManager.Instance.SetCanStartQuest($"{tutorialIds[curQuestIdx]}");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            QuestManager.Instance.ProgressQuest($"{tutorialIds[curQuestIdx]}");
        }
    }

    private void Start()
    {
        curQuestIdx = 0;
    }

    public void IncreaseQuestIdx()
    {
        curQuestIdx++;
    }
}
