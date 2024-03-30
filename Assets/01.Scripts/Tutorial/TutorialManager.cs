using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : Singleton<TutorialManager>
{
    private int curQuestIdx = 0;
    public int CurTutoQuestIdx => curQuestIdx;

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

    public string GetNextTutorialQuest()
    {
        IncreaseQuestIdx(); // 튜토리얼 퀘스트는 순서대로 해야 하니까 idx 증가

        if (tutorialIds[CurTutoQuestIdx] != null)
        {
            return tutorialIds[CurTutoQuestIdx];
        }
        else
        {
            Debug.Log("튜토리얼 퀘스트 끝이다");
            return null;
        }
    }
}
