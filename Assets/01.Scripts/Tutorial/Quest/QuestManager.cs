using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField]
    private QuestDataSO _questDataSO;

    private Dictionary<string, QuestData> _allQuestsData = new Dictionary<string, QuestData>();
    private Dictionary<string, Quest> _curInprogressQuests = new();
    private Dictionary<string, Quest> _canStartQuests = new();

    private DialogSystem _dialogSystem;
    private QuestUI _questUI;
    private CostUI _costUI;

    public override void Awake()
    {
        base.Awake();

        _dialogSystem = Object.FindAnyObjectByType<DialogSystem>();
        _questUI = Object.FindAnyObjectByType<QuestUI>();
        _costUI = Object.FindAnyObjectByType<CostUI>();
    }

    private void Start()
    {
        LoadQuestData();
    }

    private void LoadQuestData()
    {
        _allQuestsData.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++) //So�� �ִ� �� ��ųʸ��� �߰���.
        {
            _allQuestsData.Add(_questDataSO.QuestDatas[i].Id,
                               _questDataSO.QuestDatas[i]);
        }
    }

    public void SetCanStartQuest(string questId)
    {
        QuestData questData = GetQuestData(questId);
        // ���ο� ����Ʈ ����
        Quest quest = new Quest(questData);
        _canStartQuests.Add(questId, quest);

        quest.SetQuestState(QuestState.CanStart);

        _questUI.CreateScrollViewUI(quest); // ����Ʈ UI�� ����Ʈ �߰��ϴ� �ڵ�

        if (quest.QuestDataCompo.TutorialQuestInfo.IsTutorialQuest)
        {
            StartTutorial(questId);
        }
    }

    public void StartTutorial(string questId)
    {
        if (questId == null)
        {
            return;
        }

        Quest quest = GetCanStartQuests(questId);

        #region DebugByCase
        if (TutorialManager.Instance.CurTutoQuestIdx != quest.QuestDataCompo.TutorialQuestInfo.TutorialQuestIdx) //Ʃ�丮�� ����Ʈ�� ������� �� ������
        {
            Debug.Log($"��... �ʴ� ���� {TutorialManager.Instance.CurTutoQuestIdx}��° Ʃ�丮�� ����Ʈ�� �ؾ��ϴµ�" +
                $"{quest.QuestDataCompo.TutorialQuestInfo.TutorialQuestIdx}��° ����Ʈ�� {quest.QuestId}�� �Ϸ��� ���ݾ�;; ������");
            return;
        }
        #endregion

        _dialogSystem.Begin(quest.QuestDataCompo.TutorialQuestInfo.TutorialTexts); //Ʃ�丮�� �ؽ�Ʈ �߰�
        StartQuest(quest.QuestId); // Ʃ�丮���� ��ư ������ ������ �ƴ϶� �׳� �����ϰ�
    }

    public void StartQuest(string questId) //����Ʈ ����
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetCanStartQuests(questId);

        #region DebugByCase
        switch (quest.QuestStateEnum)
        {
            case QuestState.Running:
                Debug.Log($"{quest.QuestId}�� �̹� �������� ����Ʈ�� ������;;");
                return;
            case QuestState.Finish:
                Debug.Log($"{quest.QuestId}�� �̹� ������ ����Ʈ�� ������;;");
                return;
            case QuestState.Locked:
                Debug.Log($"{quest.QuestId}�� ���� ���ϴ� ����Ʈ�� ������;;");
                return;
            case QuestState.CanFinish:
                Debug.Log($"{quest.QuestId}�� �̹� �������� ����Ʈ�� ������;;");
                break;
        }
        #endregion

        Debug.Log($"{questData.Id} ����Ʈ ������");

        _canStartQuests.Remove(questId); // ������ �����Ͽ����� ���۰��� ����Ʈ���� ����

        for (int i = 0; i < quest.QuestDataCompo.QuestGoalInfo.Length; i++)
        {
            quest.QuestGoalList.Add(new QuestGoal
                (questData.QuestGoalInfo[i].QuestGoalType,
                 questData.QuestGoalInfo[i].RequiredAmount,
                 questData.QuestGoalInfo[i].GoalId)); // ��ǥ �߰�
        }

        quest.SetQuestState(QuestState.Running); // ����Ʈ ���� ������Ʈ
        _curInprogressQuests.Add(quest.QuestId, quest); // Ȱ�� ����Ʈ�� �߰�

        _questUI.UpdatePopUpQuestUI(quest); // ����Ʈ UI���� ������Ʈ
        SignalHub.OnStartQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ

    }

    public void ProgressQuest(string questId, string goalID) //����Ʈ�� ����Ǿ�����. ex: ������ �Ծ�����.
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetRunningQuest(questId);

        #region DebugByCase
        switch (quest.QuestStateEnum)
        {
            case QuestState.Finish:
                Debug.Log($"{questData.Id}�� �̹� ������ ����Ʈ�� ������;;");
                return;
            default:
                if(quest.QuestStateEnum == QuestState.CanFinish)
                {
                    Debug.Log("���� �̰� �Ϸ� ������");
                    return;
                }
                if (quest.QuestStateEnum != QuestState.Running)
                {
                    Debug.Log($"�̰� ���� ���� �� �ߴµ��� �� ���� {questData.QuestStateEnum}��"); 
                    return;
                }
                break;
        }
        #endregion

        quest.UpdateCondition(goalID);
        SignalHub.OnProgressQuestEvent?.Invoke(); // ����Ʈ ���� �̺�Ʈ

        _questUI.UpdateQuestUIToProgress(quest);

        if (quest.IsCompleted()) // �Ϸ��ߴٸ�
        {
            _questUI.UpdatePopUpQuestUI(quest);
            _questUI.SetCautionBoxImage(true);

            if (questData.TutorialQuestInfo.IsTutorialQuest) // Ʃ�丮���̸� �ٷ� �Ϸ�ó��
            {
                EndQuest(questId);
                return;
            }

            quest.SetQuestState(QuestState.CanFinish); // ����Ʈ �Ϸᰡ���ϰ�
        }
        else // ���� ���Ҵٸ�
        {
            for (int i = 0; i < questData.QuestGoalInfo.Length; i++)
            {
                Debug.Log($"���� ���� {questData.Id}����Ʈ�� {i + 1}��° ����Ʈ" +
                $"{quest.QuestGoalList[i].RequiredAmount}�� �߿� {quest.QuestGoalList[i].CurrentAmount}�� ��");
            }
            
        }
    }

    public void EndQuest(string questId)
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetRunningQuest(questId);

        Debug.Log($"{questData.Id} ����Ʈ ����");

        quest.SetQuestState(QuestState.Finish); // ����Ʈ�� �Ϸ�ó�� ���ְ�
        _questUI.UpdatePopUpQuestUI(quest);

        _curInprogressQuests.Remove(questData.Id); //���� ������ ����Ʈ ����Ʈ���� ����

        SignalHub.OnEndQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ

        _costUI.CostTween(questData.QuestRewardInfo.RewardCount, true, _questUI.QuestInfoUICompo.RewardPos.position);
        _questUI.QuestInfoUICompo.OffCanvasGroups();

        if (questData.TutorialQuestInfo.IsTutorialQuest)
        {
            SetCanStartQuest(TutorialManager.Instance.GetNextTutorialQuest()); // ���� Ʃ�丮�� ����
        }

        //_questUI.RemoveQuestContentUI(questData.Id); // �Ϸ�� ����Ʈ UI�� �߰�
    }

    public QuestData GetQuestData(string questId) => _allQuestsData[questId];
    public Quest GetRunningQuest(string questId) => _curInprogressQuests[questId];
    public Quest GetCanStartQuests(string questId) => _canStartQuests[questId];

    public bool IsQuestActive(string questId)
    {
        QuestData questData = _allQuestsData[questId];
        if (questData.QuestStateEnum.Equals(QuestState.Running))
        {
            return true;
        }
        return false;
    }
}
