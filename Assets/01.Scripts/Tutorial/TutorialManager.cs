using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private QuestData _curQuestData => _questDataSO.QuestDatas[curQuestIdx];

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
        QuestManager.Instance.SetCanStartQuest(_curQuestData.Id);
    }

    public void CurTutorialProgressQuest(QuestGoalIdx goalIdx)
    {
        QuestManager.Instance.ProgressQuest(_curQuestData.Id, _curQuestData.QuestGoalInfo[(int)goalIdx].GoalId); // 이거 인덱스가 바껴야됨
        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest(goalIdx);

        for(int i = 0; i < _curQuestData.QuestGoalInfo.Length; i++)
        {
            if (_curQuestData.QuestGoalInfo[i].QuestGoalType == QuestGoalType.BattleWin)
            {
                SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest(goalIdx);
            }
        }
        //if (curQuestIdx == 1 || curQuestIdx == 2 || curQuestIdx == 3 || // 일단 퀘스트. 곰 컷신일때 같이 나온다 수정 해야됨
        //    curQuestIdx == 4 || curQuestIdx == 5)
        //{
        //    Debug.Log("여기서는안해버리고");
        //    return;
        //}
    }

    public void IncreaseQuestIdx()
    {
        curQuestIdx++;
    }

    public string GetNextTutorialQuest()
    {
        IncreaseQuestIdx(); // 튜토리얼 퀘스트는 순서대로 해야 하니까 idx 증가

        if (_curQuestData.Id != null)
        {
            return _curQuestData.Id;
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
