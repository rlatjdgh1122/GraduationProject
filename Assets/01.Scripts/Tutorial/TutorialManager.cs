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
        IncreaseQuestIdx(); // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����

        if (tutorialIds[CurTutoQuestIdx] != null)
        {
            return tutorialIds[CurTutoQuestIdx];
        }
        else
        {
            Debug.Log("Ʃ�丮�� ����Ʈ ���̴�");
            return null;
        }
    }
}
