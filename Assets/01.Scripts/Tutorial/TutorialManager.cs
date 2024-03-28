using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : Singleton<TutorialManager>
{
    private int curQuestIdx = 0;
    public int CurQuestIdx => curQuestIdx;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            QuestManager.Instance.SetCanStartQuest("TutoTest");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            QuestManager.Instance.ProgressQuest("TutoTest");
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
