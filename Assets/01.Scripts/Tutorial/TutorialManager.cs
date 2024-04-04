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

    [SerializeField]
    private string[] goalIds;

    private void Start()
    {
        curQuestIdx = 0;

        StartCurTutorialQuest();
        SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest();
    }

    public void StartCurTutorialQuest()
    {
        QuestManager.Instance.SetCanStartQuest(tutorialIds[curQuestIdx]);
    }

    public void CurTutorialProgressQuest()
    {
        QuestManager.Instance.ProgressQuest(tutorialIds[curQuestIdx], goalIds[curQuestIdx]);

        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest();

        if (curQuestIdx == 1 || curQuestIdx == 2 || curQuestIdx == 3 || // 일단 퀘스트. 곰 컷신일때 같이 나온다 수정 해야됨
            curQuestIdx == 4 || curQuestIdx == 5)
        {
            Debug.Log("여기서는안해버리고");
            return;
        }

        Debug.Log($"지금 라운드 {curQuestIdx}인데 이 다음 이벤트는 전투 끝나고 나올 예정");

        SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest();

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

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest();
    }
}
