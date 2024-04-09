using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QuestGoalIdx
{
    First = 0,
    Second = 1,
}

public class TutorialManager : Singleton<TutorialManager>
{
    private int curQuestIdx = 0;
    public int CurTutoQuestIdx => curQuestIdx;

    [SerializeField]
    private QuestDataSO _questDataSO;

    private QuestData curQuestData => _questDataSO.QuestDatas[curQuestIdx];

    private void Start()
    {
        curQuestIdx = 0;

        StartCurTutorialQuest();
        SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest(QuestGoalIdx.Second);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CurTutorialProgressQuest(QuestGoalIdx.First);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            CurTutorialProgressQuest(QuestGoalIdx.Second);
        }
    }

    public void StartCurTutorialQuest()
    {
        QuestManager.Instance.SetCanStartQuest(curQuestData.Id);
    }

    public void CurTutorialProgressQuest(QuestGoalIdx goalIdx)
    {
        QuestManager.Instance.ProgressQuest(curQuestData.Id, curQuestData.QuestGoalInfo[(int)goalIdx].GoalId); // 이거 인덱스가 바껴야됨

        //SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest(goalIdx);

        //if (curQuestIdx == 1 || curQuestIdx == 2 || curQuestIdx == 3 || // 일단 퀘스트. 곰 컷신일때 같이 나온다 수정 해야됨
        //    curQuestIdx == 4 || curQuestIdx == 5)
        //{
        //    Debug.Log("여기서는안해버리고");
        //    return;
        //}

        //Debug.Log($"지금 라운드 {curQuestIdx}인데 이 다음 이벤트는 전투 끝나고 나올 예정");

        //SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest(goalIdx);

    }

    public void IncreaseQuestIdx()
    {
        curQuestIdx++;
    }

    public string GetNextTutorialQuest()
    {
        IncreaseQuestIdx(); // 튜토리얼 퀘스트는 순서대로 해야 하니까 idx 증가

        if (curQuestData.Id != null)
        {
            return curQuestData.Id;
        }
        else
        {
            Debug.Log("튜토리얼 퀘스트 끝이다");
            return null;
        }
    }

    private void OnEnable()
    {
        foreach(QuestGoalIdx idx in Enum.GetValues(typeof(QuestGoalIdx)))
        {
            SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest(idx);
        }
    }
}
