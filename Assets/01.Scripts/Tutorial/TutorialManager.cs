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
        QuestManager.Instance.ProgressQuest(curQuestData.Id, curQuestData.QuestGoalInfo[(int)goalIdx].GoalId); // �̰� �ε����� �ٲ��ߵ�

        //SignalHub.OnBattlePhaseEndEvent -= () => CurTutorialProgressQuest(goalIdx);

        //if (curQuestIdx == 1 || curQuestIdx == 2 || curQuestIdx == 3 || // �ϴ� ����Ʈ. �� �ƽ��϶� ���� ���´� ���� �ؾߵ�
        //    curQuestIdx == 4 || curQuestIdx == 5)
        //{
        //    Debug.Log("���⼭�¾��ع�����");
        //    return;
        //}

        //Debug.Log($"���� ���� {curQuestIdx}�ε� �� ���� �̺�Ʈ�� ���� ������ ���� ����");

        //SignalHub.OnBattlePhaseEndEvent += () => CurTutorialProgressQuest(goalIdx);

    }

    public void IncreaseQuestIdx()
    {
        curQuestIdx++;
    }

    public string GetNextTutorialQuest()
    {
        IncreaseQuestIdx(); // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����

        if (curQuestData.Id != null)
        {
            return curQuestData.Id;
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
