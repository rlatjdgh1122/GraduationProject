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
        QuestManager.Instance.ProgressQuest(_curQuestData.Id, _curQuestData.QuestGoalInfo[(int)goalIdx].GoalId); // �̰� �ε����� �ٲ��ߵ�
        SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest(goalIdx);

        for(int i = 0; i < _curQuestData.QuestGoalInfo.Length; i++)
        {
            if (_curQuestData.QuestGoalInfo[i].QuestGoalType == QuestGoalType.BattleWin)
            {
                SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest(goalIdx);
            }
        }
        //if (curQuestIdx == 1 || curQuestIdx == 2 || curQuestIdx == 3 || // �ϴ� ����Ʈ. �� �ƽ��϶� ���� ���´� ���� �ؾߵ�
        //    curQuestIdx == 4 || curQuestIdx == 5)
        //{
        //    Debug.Log("���⼭�¾��ع�����");
        //    return;
        //}
    }

    public void IncreaseQuestIdx()
    {
        curQuestIdx++;
    }

    public string GetNextTutorialQuest()
    {
        IncreaseQuestIdx(); // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����

        if (_curQuestData.Id != null)
        {
            return _curQuestData.Id;
        }
        else
        {
            Debug.Log("Ʃ�丮�� ����Ʈ ���̴�");
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
