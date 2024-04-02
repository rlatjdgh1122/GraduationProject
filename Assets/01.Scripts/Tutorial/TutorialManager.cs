using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : Singleton<TutorialManager>, IQuestTriggerObj
{
    private int curQuestIdx = 0;
    public int CurTutoQuestIdx => curQuestIdx;

    public string[] QuestIds { get; set; }
    public bool isRunning { get; set; }

    [SerializeField]
    private string[] tutorialIds;

    private void Start()
    {
        curQuestIdx = 0;

        //StartCurTutorialQuest();
        SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest();
    }

    public void StartCurTutorialQuest()
    {
        QuestManager.Instance.SetCanStartQuest(tutorialIds[curQuestIdx]);
    }

    public void CurTutorialProgressQuest()
    {
        QuestManager.Instance.ProgressQuest(tutorialIds[curQuestIdx]);

        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest();

        if (curQuestIdx == 1 || curQuestIdx == 2 || // �ϴ� ����Ʈ
            curQuestIdx == 3 || curQuestIdx == 4 ||
            curQuestIdx == 5 || curQuestIdx == 6 ||
            curQuestIdx == 7)
        {
            return;
        }

        SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest();

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

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest();
    }
}
